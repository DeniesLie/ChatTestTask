namespace Core.VIewModels.Messages;

public class MessageUpdateViewModel
{
    public int Id { get; set; }
    public int ChatroomId { get; set; }
    public string? Text { get; set; }
}