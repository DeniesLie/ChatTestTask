using Core.VIewModels.Messages;
using FluentValidation;

namespace WebApi.Validators;

public class MessageUpdateValidator : AbstractValidator<MessageUpdateViewModel>
{
    public MessageUpdateValidator()
    {
        RuleFor(vm => vm.Text)
            .NotNull()
            .NotEmpty()
            .Length(1, 600)
            .WithMessage("Message length must be between 1 and 600 characters");
        
        RuleFor(vm => vm.Id)
            .GreaterThan(0)
            .WithMessage("Invalid message id");
    }
}