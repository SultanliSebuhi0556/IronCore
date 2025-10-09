using FluentValidation;
using PcAsCloud.BL.DTOs.Channel;

namespace PcAsCloud.BL.Validators.Channel;

public class ChannelCreateDTOValidator : AbstractValidator<ChannelCreateDTO>
{
    public ChannelCreateDTOValidator()
    {
        RuleFor(x => x.CurrentUser)
            .NotNull().WithMessage("Current user is required.");

        When(x => x.IsDirect, () =>
        {
            RuleFor(x => x.TargertUser)
                .NotNull().WithMessage("A direct channel must have a target user!");

            RuleFor(x => x.TargertUser)
                .Must((dto, targetUser) => targetUser?.Id != dto.CurrentUser?.Id)
                .WithMessage("Cannot create a direct channel with yourself!")
                .When(x => x.TargertUser != null && x.CurrentUser != null);
        });

        When(x => !x.IsDirect, () =>
        {
            RuleFor(x => x.ChannelName)
                .NotEmpty().WithMessage("An indirect channel must have a channel name!")
                .Length(3, 100).WithMessage("Channel name must be between 3 and 100 characters.")
                .Matches("^[a-zA-Z0-9-_ ]+$").WithMessage("Channel name can only contain letters, numbers, spaces, hyphens, and underscores.");
        });

        When(x => x.IsDirect, () =>
        {
            RuleFor(x => x.ChannelName)
                .Empty().WithMessage("Direct channels cannot have a custom channel name.")
                .When(x => !string.IsNullOrWhiteSpace(x.ChannelName));
        });
    }
}
