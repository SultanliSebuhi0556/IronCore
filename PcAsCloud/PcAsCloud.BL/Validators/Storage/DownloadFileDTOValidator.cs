using FluentValidation;
using PcAsCloud.BL.DTOs.Storage;

namespace PcAsCloud.BL.Validators.Storage;
public class DownloadFileDTOValidator : AbstractValidator<DownloadFileDTO>
{
    public DownloadFileDTOValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("FileName is required");
    }
}