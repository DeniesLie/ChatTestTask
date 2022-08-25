using Core.Entities;
using Core.Interfaces.Repositories;

namespace DataAccess.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}