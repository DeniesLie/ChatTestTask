using Core.Enums;

namespace Core.Entities;

public class Chatroom
{
    public int Id { get; set; }
    public ChatType Type { get; set; }
    public string? Name { get; set; }

    public IEnumerable<UserChatroom> UserChatrooms { get; set; } = new List<UserChatroom>();
}