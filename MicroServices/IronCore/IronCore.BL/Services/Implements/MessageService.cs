using AutoMapper;
using FluentValidation;
using IronCore.BL.DTOs.Message;
using IronCore.BL.DTOs.RabbitMQDTOs;
using IronCore.BL.Exceptions.CommonExceptions;
using IronCore.BL.ExternalServices.Instances;
using IronCore.BL.Services.Instances;
using IronCore.CORE.Entities;
using IronCore.CORE.Enums;
using IronCore.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace IronCore.BL.Services.Implements;
public class MessageService(
    AppDbContext _context,
    IStorageService _storageService,
    IHttpContextAccessor _httpContextAccessor,
    UserManager<AppUser> _userManager,
    IValidator<MessageCreateDTO> _validator,
    IRabbitMQPublisher _publisher,
    IMapper _mapper) : IMessageService
{
    public async Task<MessageCreateResponseDTO?> CreateMessageAsync(MessageCreateDTO dto, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var user = await _getCurrentUserAsync();

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

        await _context.AddAsync(message, cancellationToken);

        var messageDto = new MessageDTO
        {
            Id = message.Id.ToString(),
            Content = message.Content,
            IsRead = message.IsRead,
            ChannelId = message.ChannelId.ToString(),
            StorageId = message.StorageId?.ToString(),
            SendedById = message.SendedById
        };
        var json = JsonSerializer.Serialize(messageDto);
        await _publisher.PublishMessagesAsync(json, "message.create", cancellationToken);

        var response = new MessageCreateResponseDTO { Id = message.Id.ToString() };

        if (dto.File != null)
        {
            var result = await _storageService.SaveFileAsync(new DTOs.Storage.UploadFileDTO() { File = dto.File, NewFolderName = Path.Combine("@ChannelFiles", channel.Id.ToString()), NewFileName = Guid.NewGuid().ToString() }, cancellationToken);
            message.StorageId = result.StorageId;
            response.FileName = result.FileName;
            response.StorageId = result.StorageId.ToString();
        }

        await _context.SaveChangesAsync(cancellationToken);
        return response;
    }

    public async Task<bool> ArchiveUnarchiveMessageAsync(string id, CancellationToken cancellationToken)
    {
        var target = await _context.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
        if (target == null) throw new NotFoundException<Message>();

        var user = await _getCurrentUserAsync();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        if (target.SendedById != user.Id && !rolesOfUser.Contains(nameof(UserRoles.Admin))) throw new Exception("cant delete this message "); //TODO: ex

        if (target.IsArchived) target.ArchiveDate = DateTime.UtcNow;
        else target.ArchiveDate = null;

        target.IsArchived = target.IsArchived ? false : true;

        await _context.SaveChangesAsync(cancellationToken);
        return target.IsArchived;
    }

    public async Task DeleteMessageAsync(string id, CancellationToken cancellationToken)
    {
        var target = await _context.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
        if (target == null) throw new NotFoundException<Message>();

        var user = await _getCurrentUserAsync();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        if (target.SendedById != user.Id && !rolesOfUser.Contains(nameof(UserRoles.Admin))) throw new Exception("cant delete this message "); //TODO: ex

        var json = JsonSerializer.Serialize(target.Id.ToString());
        await _publisher.PublishMessagesAsync(json, "message.delete", cancellationToken);

        await Task.Run(() => _context.Remove(target));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<MessageGetDTO>> GetAllMessagesByChannelIdAsync(string channelId, CancellationToken cancellationToken)
    {
        var channel = await _context.Channels.Include(x => x.ChannelUsers).Include(x => x.Messages).FirstOrDefaultAsync(x => x.Id.ToString() == channelId, cancellationToken);
        if (channel == null) throw new NotFoundException<Channel>();

        if (channel.IsDirect)
        {
            var user = await _getCurrentUserAsync();
            if (!channel.ChannelUsers.Any(x => x.AppUser == user)) throw new Exception("u are not in this channel"); //TODO: ex
            channel.Messages.Where(x => x.SendedById != user.Id).ToList().ForEach(x => x.IsRead = true);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<IEnumerable<MessageGetDTO>>(channel.Messages);
    }

    public async Task<MessageGetDTO> GetMessageByIdAsync(string id, CancellationToken cancellationToken)
    {
        var message = await _context.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
        if (message == null) throw new NotFoundException<Message>();

        var user = await _getCurrentUserAsync();
        var channel = await _context.Channels.FirstOrDefaultAsync(x => x.Id == message.ChannelId);
        if (channel!.IsDirect && message.SendedById != user.Id)
        {
            message.IsRead = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return _mapper.Map<MessageGetDTO>(message);
    }

    private async Task<AppUser> _getCurrentUserAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        if (user == null) throw new NotFoundException<AppUser>();
        return user;
    }

    public async Task<IEnumerable<MessageGetDTO>> GetMessageBySearchAsync(string channelId, string searchText, CancellationToken cancellationToken)
    {
        var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
        var url = $"https://localhost:7173/api/Message/GetMessages?channelId={Uri.EscapeDataString(channelId)}&searchText={Uri.EscapeDataString(searchText)}";
        var result = await client.GetStringAsync(url);
        if (result == "[]") throw new NotFoundException<MessageGetDTO>();
        return JsonSerializer.Deserialize<IEnumerable<MessageGetDTO>>(result)!;
    }
}