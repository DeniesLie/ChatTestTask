using Core.Enums;

namespace Core.VIewModels.Chatrooms;

public class ChatroomGetViewModel
{
    public int Id { get; set; }
    public ChatType ChatType { get; set; }
    public string? Name { get; set; }
}