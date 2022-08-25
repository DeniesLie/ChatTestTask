using Core.Entities;
using Core.Interfaces.Repositories;

namespace DataAccess.Repositories;

public class UserChatroomRepository : Repository<UserChatroom>, IUserChatroomRepository
{
    public UserChatroomRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}