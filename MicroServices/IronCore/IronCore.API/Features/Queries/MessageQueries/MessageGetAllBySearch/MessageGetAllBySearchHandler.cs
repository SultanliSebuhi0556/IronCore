using AutoMapper;
using IronCore.BL.Services.Instances;
using MediatR;

namespace IronCore.API.Features.Queries.MessageQueries.MessageGetAllBySearch;
public class GetAllMessageBySearchHandler(IMessageService _messageService, IMapper _mapper) : IRequestHandler<MessageGetAllBySearchRequest, IEnumerable<MessageGetAllBySearchResult>>
{
    async Task<IEnumerable<MessageGetAllBySearchResult>> IRequestHandler<MessageGetAllBySearchRequest, IEnumerable<MessageGetAllBySearchResult>>.Handle(MessageGetAllBySearchRequest request, CancellationToken cancellationToken)
    {
        var result = await _messageService.GetMessageBySearchAsync(request.ChannelId, request.SearchText, cancellationToken);
        return _mapper.Map<IEnumerable<MessageGetAllBySearchResult>>(result);
    }
}