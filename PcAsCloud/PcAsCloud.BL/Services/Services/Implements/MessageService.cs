using AutoMapper;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;

namespace PcAsCloud.BL.Services.Services.Implements;
public class MessageService(IMessageRepository _messageRepository, IMapper _mapper) : IMessageService
{

    public Task<string> CreateMessageAsync(AppUser currentUser, string channelId)
    {
        throw new NotImplementedException();
    }

    public Task ArchiveUnarchiveMessageAsync(string id)
    {
        var target = await _messageRepository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Message>();
        target.IsArchived = target.IsArchived ? false : true;
        await _channelRepository.SaveChangesAsync();
    }

    public async Task DeleteMessageAsync(string id)
    {
        var target = await _messageRepository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Message>();
        await _messageRepository.DeleteAsync(target);
        await _messageRepository.SaveChangesAsync();
    }

    public Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<MessageGetDTO> GetMessageByIdAsync(string id)
    {
        var message = await _messageRepository.GetByIdAsync(id);
        if (message == null) throw new NotFoundException<Message>();
        return _mapper.Map<MessageGetDTO>(message);
    }
}
