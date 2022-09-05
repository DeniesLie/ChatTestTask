using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories;

public class ChatroomRepository : Repository<Chatroom>, IChatroomRepository
{
    public ChatroomRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<Chatroom>> GetChatroomsByUserId(int userId)
    {
        var chatrooms = (await DbContext.Chatrooms
                .Include(cr => cr.UserChatrooms)
                    .ThenInclude(ur => ur.Messages)
                .Include(cr => cr.UserChatrooms)
                    .ThenInclude(ur => ur.User)
                .Where(cr => cr.UserChatrooms.Any(ur => ur.UserId == userId))
                .Select(cr => new
                {
                    chatroom = cr,
                    lastMessage = cr.UserChatrooms
                                .SelectMany(ur => ur.Messages)
                                .OrderByDescending(m => m.SentAt)
                                .FirstOrDefault()
                }).ToListAsync())
                .Select(cr => {
                    cr.chatroom.LastMessage = cr.lastMessage;
                    return cr.chatroom;
                }).ToList();
            
        return chatrooms;
    }

}