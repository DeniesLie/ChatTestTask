using Core.Enums;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

[Authorize]
public class MessageHub : Hub
{
    public async Task JoinGroup(int chatroomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatroomId.ToString());
    }
}