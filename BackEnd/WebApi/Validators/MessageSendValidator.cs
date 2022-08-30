using Core.VIewModels.Messages;
using FluentValidation;

namespace WebApi.Validators;

public class MessageSendToChatroomValidator : AbstractValidator<MessageToChatroomSendViewModel>
{
    public MessageSendToChatroomValidator()
    {
        RuleFor(vm => vm.Text)
            .NotNull()
            .NotEmpty()
            .Length(1, 600)
            .WithMessage("Message length must be between 1 and 600 characters");
        
        RuleFor(vm => vm.ChatroomId)
            .GreaterThan(0)
            .WithMessage("Invalid chatroom id");
        
        RuleFor(vm => vm.RepliedMessageId)
            .GreaterThan(0)
            .WithMessage("Invalid replied message id");
    }
}

public class PrivateMessageSendValidator : AbstractValidator<PrivateMessageSendViewModel>
{
    public PrivateMessageSendValidator()
    {
        RuleFor(vm => vm.Text)
            .NotNull()
            .NotEmpty()
            .Length(1, 600)
            .WithMessage("Message length must be between 1 and 600 characters");
        
        RuleFor(vm => vm.ReceiverId)
            .GreaterThan(0)
            .WithMessage("Invalid receiver id");
        
        RuleFor(vm => vm.RepliedMessageId)
            .GreaterThan(0)
            .WithMessage("Invalid replied message id");
    }
}