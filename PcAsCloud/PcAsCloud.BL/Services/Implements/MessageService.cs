using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcAsCloud.BL.DTOs.Message;
using PcAsCloud.BL.Enums;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Instances;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.DAL.Context;

namespace PcAsCloud.BL.Services.Services.Implements;
public class MessageService(
    AppDbContext _context,
    IChannelServices _channelServices,
    IStorageService _storageService,
    IHttpContextAccessor _httpContextAccessor,
    UserManager<AppUser> _userManager,
    IValidator<MessageCreateDTO> _validator,
    IMapper _mapper) : IMessageService
{
    public async Task<MessageCreateResponseDTO?> CreateMessageAsync(MessageCreateDTO dto, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var message = new Message
        {
            Content = dto.Content,
            SendedBy = user,
            ChannelId = Guid.Parse(dto.ChannelId),
        };

        var channel = await _context.Channels.Include(x => x.ChannelUsers).FirstOrDefaultAsync(x => x.Id == message.ChannelId, cancellationToken);
        if (channel == null) throw new NotFoundException<Channel>();

        if (!channel.ChannelUsers.Any(x => x.AppUser == message.SendedBy))
            throw new Exception("cant send message in this chat"); //TODO: ex

        if (dto.File != null)
        {
            var newFileName = $"{dto.ChannelId}_{Guid.NewGuid()}";
            //var resultPath = await _storageService.SaveFileAsync(dto.File, newFileName, cancellationToken);
            //message.FileUrl = resultPath;
        }

        await _context.AddAsync(message, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new MessageCreateResponseDTO
        {
            Id = message.Id.ToString(),
            FilePath = message.FileUrl
        };
    }

    public async Task<bool> ArchiveUnarchiveMessageAsync(string id, CancellationToken cancellationToken)
    {
        var target = await _context.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
        if (target == null) throw new NotFoundException<Message>();

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        if (target.SendedById != user.Id && !rolesOfUser.Contains(nameof(UserRoles.Admin))) throw new Exception("cant delete this message "); //TODO: ex

        target.IsArchived = target.IsArchived ? false : true;
        await _context.SaveChangesAsync(cancellationToken);
        return target.IsArchived;
    }

    public async Task DeleteMessageAsync(string id, CancellationToken cancellationToken)
    {
        var target = await _context.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
        if (target == null) throw new NotFoundException<Message>();

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        if (target.SendedById != user.Id && !rolesOfUser.Contains(nameof(UserRoles.Admin))) throw new Exception("cant delete this message "); //TODO: ex

        await Task.Run(() => _context.Remove(target));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId, CancellationToken cancellationToken)
    {
        var channel = await _context.Channels.Include(x => x.Messages).ThenInclude(x => x.SendedBy).FirstOrDefaultAsync(x => x.Id.ToString() == channelId, cancellationToken);
        if (channel == null) throw new NotFoundException<Channel>();

        return _mapper.Map<IEnumerable<MessageGetDTO>>(channel.Messages);
    }

    public async Task<MessageGetDTO> GetMessageByIdAsync(string id, CancellationToken cancellationToken)
    {
        var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
        if (message == null) throw new NotFoundException<Message>();
        return _mapper.Map<MessageGetDTO>(message);
    }
}