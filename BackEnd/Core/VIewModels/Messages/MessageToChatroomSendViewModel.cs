namespace Core.VIewModels.Messages;

public class MessageToChatroomSendViewModel
{
    public string? Text { get; set; }
    public int ChatroomId { get; set; }
    public int? RepliedMessageId { get; set; }
}