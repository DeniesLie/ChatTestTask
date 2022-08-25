using Core.Entities;
using Core.Interfaces.Repositories;

namespace DataAccess.Repositories;

public class ChatroomRepository : Repository<Chatroom>, IChatroomRepository
{
    public ChatroomRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}