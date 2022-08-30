using Application.Hubs;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.VIewModels.Messages;
using Microsoft.AspNetCore.SignalR;

namespace Application.Services;

public class SignalrNotificationService : INotificationService
{
    private readonly IHubContext<MessageHub> _hubContext;
    private readonly IChatroomService _chatroomService;
    private readonly IMapper _mapper;
    
    public SignalrNotificationService(
        IHubContext<MessageHub> hubContext, 
        IChatroomService chatroomService,
        IMapper mapper)
    {
        _hubContext = hubContext;
        _chatroomService = chatroomService;
        _mapper = mapper;
    }

    public async Task SendMessageAsync(Message message)
    {
        await SendNotificationAsync<MessageGetViewModel>("getMessage", message);
    }

    public async Task NotifyAboutMessageDeletionAsync(Message message)
    {
        await SendNotificationAsync<MessageDeleteViewModel>("deleteMessage", message);
    }

    public async Task NotifyAboutMessageEditingAsync(Message message)
    {
        await SendNotificationAsync<MessageUpdateViewModel>("editMessage", message);
    }

    private async Task SendNotificationAsync<TViewModel>(string method, Message message)
    {
        var chatroom = await _chatroomService.GetByIdAsync(message.ChatroomId);

        if (chatroom is null)
            throw new NotFoundException($"Chatroom with id {message.ChatroomId} was not found");
        
        IClientProxy clientProxy;
        
        if (chatroom.Type == ChatType.Private)
        {
            var receiverId = chatroom.UserChatrooms
                .FirstOrDefault(ur => ur.UserId != message.SenderId)?.UserId ?? 0;

            clientProxy = _hubContext.Clients.User(receiverId.ToString());
        }
        else
            clientProxy = _hubContext.Clients.Group(message.ChatroomId.ToString());

        var messageVm = _mapper.Map<TViewModel>(message);

        await clientProxy.SendAsync(method, messageVm);
    }
}