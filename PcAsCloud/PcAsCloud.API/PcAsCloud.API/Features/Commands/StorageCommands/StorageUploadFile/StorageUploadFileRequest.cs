using MediatR;

namespace PcAsCloud.API.Features.Commands.StorageCommands.StorageUploadFile;
public class StorageUploadFileRequest : IRequest<StorageUploadFileResponse>
{
    public IFormFile File { get; set; }
    public string? NewFileName { get; set; }
}