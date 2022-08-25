namespace Core.VIewModels.Messages;

public class MessageGetViewModel
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public DateTimeOffset SentAt { get; set; }
    public RepliedMessageGetViewModel? RepliedMessage { get; set; }
    public bool IsEdited { get; set; }
    public int SenderId { get; set; }
    public string? SenderName { get; set; }
    public int ChatroomId { get; set; }
}