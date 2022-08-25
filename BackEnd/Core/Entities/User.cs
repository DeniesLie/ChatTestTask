namespace Core.Entities;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public bool IsActive { get; set; }

    public IEnumerable<UserChatroom> UserChatrooms { get; set; } = new List<UserChatroom>();
}