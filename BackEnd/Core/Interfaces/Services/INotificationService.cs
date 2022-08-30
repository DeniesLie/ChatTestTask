using Core.Entities;
using Core.VIewModels.Messages;

namespace Core.Interfaces.Services;

public interface INotificationService
{
    Task SendMessageAsync(Message message);
    Task NotifyAboutMessageDeletionAsync(Message message);
    Task NotifyAboutMessageEditingAsync(Message message);
}