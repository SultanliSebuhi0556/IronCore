using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;

namespace PcAsCloud.BL.Services.Services.Implements;
public class MessageService(
    IMessageRepository _messageRepository,
    IChannelServices _channelServices,
    IStorageService _storageService,
    IHttpContextAccessor _httpContextAccessor,
    UserManager<AppUser> _userManager,
    IValidator<MessageCreateDTO> _validator,
    IMapper _mapper) : IMessageService
{
    public async Task<string?> CreateMessageAsync(MessageCreateDTO dto, CancellationToken cancellationToken)
    {
        //TODO: add ct to methods in this class 

        await _validator.ValidateAndThrowAsync(dto);

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var message = new Message
        {
            Content = dto.Content,
            SendedBy = user,
            ChannelId = Guid.Parse(dto.ChannelId),
        };

        if (dto.File != null)
        {
            var newFileName = $"{dto.ChannelId}_{Guid.NewGuid()}";
            //var resultPath = await _storageService.SaveFileAsync(dto.File, newFileName, cancellationToken);
            //message.FileUrl = resultPath;
        }

        await _messageRepository.CreateAsync(message);
        await _messageRepository.SaveChangesAsync();

        return message.FileUrl;
    }

    public async Task ArchiveUnarchiveMessageAsync(string id)
    {
        var target = await _messageRepository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Message>();
        target.IsArchived = target.IsArchived ? false : true;
        await _messageRepository.SaveChangesAsync();
    }

    public async Task DeleteMessageAsync(string id)
    {
        var target = await _messageRepository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Message>();
        await _messageRepository.DeleteAsync(target);
        await _messageRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId)
    {
        var channel = await _channelServices.GetChannelByIdAsync(channelId);
        return _mapper.Map<IEnumerable<MessageGetDTO>>(channel.Messages);
    }

    public async Task<MessageGetDTO> GetMessageByIdAsync(string id)
    {
        var message = await _messageRepository.GetByIdAsync(id);
        if (message == null) throw new NotFoundException<Message>();
        return _mapper.Map<MessageGetDTO>(message);
    }
}
