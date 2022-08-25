namespace Core.VIewModels.Messages;

public class PrivateMessageSendViewModel
{
    public string? Text { get; set; }
    public int ReceiverId { get; set; }
    public int? RepliedMessageId { get; set; }
}