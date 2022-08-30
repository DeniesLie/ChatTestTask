using Core.Enums;
using Core.VIewModels.Messages;

namespace Core.VIewModels.Chatrooms;

public class ChatroomGetViewModel
{
    public int Id { get; set; }
    public ChatType ChatType { get; set; }
    public string? Name { get; set; }
    public MessageGetViewModel? LastMessage { get; set; }
}