namespace Core.Entities;

public class UserChatroom
{
    public int UserId { get; set; }
    public int ChatroomId { get; set; }

    public User? User { get; set; }
    public Chatroom? Chatroom { get; set; }
    public IEnumerable<Message> Messages { get; set; } = new List<Message>();
}