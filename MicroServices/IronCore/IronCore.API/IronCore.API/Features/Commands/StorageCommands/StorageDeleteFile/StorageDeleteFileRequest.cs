using MediatR;

namespace IronCore.API.Features.Commands.StorageCommands.StorageDeleteFile;
public class StorageDeleteFileRequest : IRequest<StorageDeleteFileResponse>
{
    public string StorageId { get; set; }
    public string? ChannelId { get; set; }
}