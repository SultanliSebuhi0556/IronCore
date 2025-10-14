using FluentValidation;
using PcAsCloud.BL.DTOs.Channel;

namespace PcAsCloud.BL.Validators.Channel;
public class ChannelCreateDTOValidator : AbstractValidator<ChannelCreateDTO>
{
    public ChannelCreateDTOValidator()
    {
        When(x => x.IsDirect, () =>
        {
            RuleFor(x => x.TargetUserId)
                .NotEmpty().WithMessage("A direct channel must have a target user ID!");
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