using Core.Entities;

namespace Core.Interfaces.Services;

public interface IChatroomService
{
    Task<IEnumerable<Chatroom>> GetByUserIdAsync(int userId);
    Task<Chatroom?> GetByIdAsync(int chatroomId);
    Task<IEnumerable<User>> GetParticipantsInChatroomAsync(int chatroomId);
    Task<Chatroom> EnsurePrivateChatroomCreatedAsync(int member1Id, int member2Id);
}