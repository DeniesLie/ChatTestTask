namespace Core.Entities;

public class Message
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTimeOffset SentAt { get; set; }
    public bool IsEdited { get; set; }
    public bool IsDeletedForSender { get; set; }
    public bool IsDeletedForEveryone { get; set; }
    
    public int? RepliedMessageId { get; set; }
    public Message? RepliedMessage { get; set; }
    public IEnumerable<Message> Replies { get; set; } = new List<Message>();
    
    public int SenderId { get; set; }
    public int ChatroomId { get; set; }
    public UserChatroom? UserChatroom { get; set; }
}