using Core.Entities;
using Core.Interfaces.Repositories;

namespace DataAccess.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}