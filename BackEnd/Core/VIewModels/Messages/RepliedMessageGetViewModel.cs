namespace Core.VIewModels.Messages;

public class RepliedMessageGetViewModel
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public string? SenderName { get; set; }
    public bool IsDeleted { get; set; }
}