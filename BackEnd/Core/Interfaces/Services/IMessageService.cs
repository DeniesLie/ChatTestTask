using Core.Entities;

namespace Core.Interfaces.Services;

public interface IMessageService
{
    Task<IEnumerable<Message>> GetAsync(int chatroomId, int readerId, int page);
    Task<Message?> GetByIdAsync(int messageId);
    Task<Message> CreatePrivateMessageAsync(Message message, int receiverId);
    Task<Message> CreateMessageToGroupAsync(Message message);
    Task<Message> EditAsync(int messageId, string text, int editorId);
    Task DeleteForSenderOnlyAsync(int messageId, int userId);
    Task DeleteForEveryoneAsync(int messageId, int userId);
}