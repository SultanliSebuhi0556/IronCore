using FluentValidation;
using PcAsCloud.BL.DTOs.Storage;

namespace PcAsCloud.BL.Validators.Storage;
public class DeleteFileDTOValidator : AbstractValidator<DeleteFileDTO>
{
    public DeleteFileDTOValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("FileName is required");
    }
}