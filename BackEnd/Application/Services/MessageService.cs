using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepo;
    private readonly IChatroomService _chatroomService;

    public MessageService(IMessageRepository messageRepo, IChatroomService chatroomService)
    {
        _messageRepo = messageRepo;
        _chatroomService = chatroomService;
    }
    
    public async Task<IEnumerable<Message>> GetAsync(int chatroomId, int readerId, int page)
    {
        const int pageSize = 20;
        
        var chatroom = await _chatroomService.GetByIdAsync(chatroomId);
        if (chatroom is null)
            throw new NotFoundException($"Chatroom with id {chatroomId} was not found");

        var readerIsInChatroom =
            (await _chatroomService.GetParticipantsInChatroomAsync(chatroomId)).Any(u => u.Id == readerId);
        
        if (!readerIsInChatroom)
            throw new UnauthorizedException();
        
        return await _messageRepo.QueryAsync(
            include: q => 
                q.Include(m => m.RepliedMessage),
            filter: m => m.ChatroomId == chatroomId 
                         && !m.IsDeletedForEveryone 
                         && !(readerId == m.SenderId && m.IsDeletedForSender), // current reader is NOT a sender who deleted message for himself
            orderBy: q => q.OrderByDescending(m => m.SentAt),
            skip: (page-1) * pageSize,
            take: pageSize,
            asNoTracking: true);
    }

    public async Task<Message?> GetByIdAsync(int messageId)
    {
        return await _messageRepo.GetFirstOrDefaultAsync(
            filter: m => m.Id == messageId,
            asNoTracking: true);
    }
    
    public async Task<Message> CreatePrivateMessageAsync(Message message, int receiverId)
    {
        var chatroom = await _chatroomService.EnsurePrivateChatroomCreatedAsync(message.SenderId, receiverId);

        message.ChatroomId = chatroom.Id;
        
        await _messageRepo.CreateAsync(message);
        await _messageRepo.SaveChangesAsync();
        
        return message;
    }
    
    public async Task<Message> CreateMessageToGroupAsync(Message message)
    {
        // check if public chatroom exist
        // TODO: add user check
        var chatroom = await _chatroomService.GetByIdAsync(message.ChatroomId);
        if (chatroom is null)
            throw new NotFoundException($"Group chatroom with id {message.ChatroomId} was not found");
        
        // check if user is member of this group
        if (!chatroom.UserChatrooms.Select(ur => ur.UserId).Contains(message.SenderId))
            throw new UnauthorizedException($"You can't write to a group you are not a member of");
        
        await _messageRepo.CreateAsync(message);
        await _messageRepo.SaveChangesAsync();
        
        return message;
    }

    public async Task<Message> EditAsync(int messageId, string text, int editorId)
    {
        var messageToUpdate = await GetByIdAsync(messageId);

        if (messageToUpdate is null)
            throw new NotFoundException($"Message with id {messageId} was not found");

        if (messageToUpdate.SenderId != editorId)
            throw new UnauthorizedException("Only sender can edit the message");
        
        messageToUpdate.Text = text;
        
        _messageRepo.Update(messageToUpdate);
        await _messageRepo.SaveChangesAsync();

        return messageToUpdate;
    }

    public async Task DeleteForSenderOnlyAsync(int messageId, int userId)
    {
        var messageToDelete = await GetByIdAsync(messageId);
        
        if (messageToDelete is null)
            throw new NotFoundException($"Message with id {messageId} was not found");

        if (messageToDelete.SenderId != userId)
            throw new UnauthorizedException("Only sender can delete the message");
        
        messageToDelete.IsDeletedForSender = true;
        _messageRepo.Update(messageToDelete);
        await _messageRepo.SaveChangesAsync();
    }

    public async Task DeleteForEveryoneAsync(int messageId, int userId)
    {
        var messageToDelete = await GetByIdAsync(messageId);
        
        if (messageToDelete is null)
            throw new NotFoundException($"Message with id {messageId} was not found");

        if (messageToDelete.SenderId != userId)
            throw new UnauthorizedException("Only sender can delete the message");
        
        messageToDelete.IsDeletedForEveryone = true;
        _messageRepo.Update(messageToDelete);
        await _messageRepo.SaveChangesAsync();
    }
}