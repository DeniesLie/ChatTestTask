using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IChatroomRepository : IRepository<Chatroom>
{
    Task<IList<Chatroom>> GetChatroomsByUserId(int userId);
}