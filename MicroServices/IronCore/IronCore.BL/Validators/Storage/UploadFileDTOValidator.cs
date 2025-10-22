using FluentValidation;
using IronCore.BL.DTOs.Storage;

namespace IronCore.BL.Validators.Storage;
public class UploadFileDTOValidator : AbstractValidator<UploadFileDTO>
{
    public UploadFileDTOValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required")
            .Must(f => f.Length > 0).WithMessage("File cannot be empty");

        RuleFor(x => x.NewFileName)
            .MaximumLength(255).WithMessage("NewFileName cannot exceed 255 characters");
    }
}