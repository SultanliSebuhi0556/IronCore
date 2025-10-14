using FluentValidation;
using Microsoft.AspNetCore.Http;
using PcAsCloud.BL.DTOs.Message;

namespace PcAsCloud.BL.Validators.Message;
public class MessageCreateDTOValidator : AbstractValidator<MessageCreateDTO>
{
    public MessageCreateDTOValidator()
    {
        RuleFor(x => x.ChannelId)
            .NotEmpty().WithMessage("Channel ID is required.");

        RuleFor(x => x.Content)
            .MaximumLength(5000).WithMessage("Message content cannot exceed 5000 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Content) || x.File != null)
            .WithMessage("A message must have a content or a file!");

        When(x => x.File != null, () =>
        {
            RuleFor(x => x.File)
                .Must(file => file!.Length > 0).WithMessage("The uploaded file is empty!")
                .Must(file => !string.IsNullOrWhiteSpace(file!.FileName)).WithMessage("The file name is invalid!")
                .Must(HaveValidFileName).WithMessage("The file name contains invalid characters!");
        });
    }

    private bool HaveValidFileName(IFormFile? file)
    {
        if (file == null) return true;

        var fileName = Path.GetFileName(file.FileName);
        return !fileName.Contains("..") && !fileName.Contains("/") && !fileName.Contains("\\");
    }
}