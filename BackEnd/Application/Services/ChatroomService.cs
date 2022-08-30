using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ChatroomService : IChatroomService
{
    private readonly IChatroomRepository _chatroomRepo;
    private readonly IUserChatroomRepository _userChatroomRepo;
    private readonly IUserService _userService;

    public ChatroomService(
        IChatroomRepository chatroomRepo, 
        IUserChatroomRepository userChatroomRepo,
        IUserService userService)
    {
        _chatroomRepo = chatroomRepo;
        _userChatroomRepo = userChatroomRepo;
        _userService = userService;
    }

    public async Task CreateAsync(Chatroom chatRoom)
    {
        // check if any of users not found (is null)
        // TODO: check if this a good practice to use await in linq queries
        var participants = chatRoom.UserChatrooms
            .Select(async ur => (await _userService.GetByIdAsync(ur.UserId)));

        if (participants.Any(user => user is null))
            throw new NotFoundException("Some of users were not found");
        
        await _chatroomRepo.CreateAsync(chatRoom);
        await _chatroomRepo.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Chatroom>> GetByUserIdAsync(int userId)
    {
        var user = await _userService.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException($"User with id {userId} was not found");

        var chatrooms = await _chatroomRepo.QueryAsync(
            include: q => q
                .Include(cr => cr.UserChatrooms)
                    .ThenInclude(ur => ur.Messages)
                .Include(cr => cr.UserChatrooms)
                    .ThenInclude(ur => ur.User),
            filter: cr => cr.UserChatrooms.Any(ur => ur.UserId == userId),
            asNoTracking: true);

        foreach (var chatroom in chatrooms)
        {
            chatroom.LastMessage = chatroom.UserChatrooms
                .SelectMany(ur => ur.Messages)
                .Where(m => !m.IsDeletedForEveryone && !(m.IsDeletedForSender && m.SenderId == userId))
                .MaxBy(m => m.SentAt);
            
            if (chatroom.Type == ChatType.Private)
            {
                chatroom.Name = chatroom.UserChatrooms.Select(ur => ur.User.Username)
                    .FirstOrDefault(name => name != user.Username);
            }
        }

        return chatrooms;
    }

    public async Task<Chatroom?> GetByIdAsync(int chatroomId)
    {
        return await _chatroomRepo.GetFirstOrDefaultAsync(
            include: q => q.Include(cr => cr.UserChatrooms),
            filter: cr => cr.Id == chatroomId);
    }

    public async Task<IEnumerable<User>> GetParticipantsInChatroomAsync(int chatroomId)
    {
        var chatroom = await GetByIdAsync(chatroomId);
        if (chatroom is null)
            throw new NotFoundException($"Chatroom with id {chatroomId} was not found");

        return (await _userChatroomRepo
                .QueryAsync(
                    filter: ur => ur.ChatroomId == chatroomId,
                    include: q =>
                        q.Include(ur => ur.User),
                    asNoTracking: true))
            .Select(ur => ur.User);
    }

    public async Task<Chatroom> EnsurePrivateChatroomCreatedAsync(int member1Id, int member2Id)
    {
        var (member1, member2) = (await _userService.GetByIdAsync(member1Id),
            await _userService.GetByIdAsync(member2Id));

        if (member1 is null)
            throw new NotFoundException($"User with id {member1Id} was not found");

        if (member2 is null)
            throw new NotFoundException($"User with id {member2Id} was not found");

        var chatroom = await _chatroomRepo.GetFirstOrDefaultAsync(
            include: q => q.Include(cr => cr.UserChatrooms),
            filter: cr =>
                cr.UserChatrooms.Select(ur => ur.UserId).Contains(member1Id)
                && cr.UserChatrooms.Select(ur => ur.UserId).Contains(member2Id)
                && cr.Type == ChatType.Private);

        if (chatroom is null)
        {
            chatroom = new Chatroom()
            {
                Type = ChatType.Private,
                UserChatrooms = new[]
                {
                    new UserChatroom() { UserId = member1Id },
                    new UserChatroom() { UserId = member2Id }
                }
            };
            await CreateAsync(chatroom);
        }

        return chatroom;
    }
}