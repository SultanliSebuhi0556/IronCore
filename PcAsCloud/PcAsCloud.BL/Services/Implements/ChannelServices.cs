using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;

namespace PcAsCloud.BL.Services.Implements;
public class ChannelServices(
    IChannelRepository _channelRepository,
    UserManager<AppUser> _userManager,
    IHttpContextAccessor _httpContextAccessor,
    IValidator<ChannelCreateDTO> _validator) : IChannelServices
{

    public async Task<string> CreateChannelAsync(ChannelCreateDTO dto)
    {
        var channel = new Channel()
        {
            IsDirect = dto.IsDirect
        };

        await _validator.ValidateAndThrowAsync(dto);

        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        if (currentUser == null) throw new NotFoundException<AppUser>();

        if (dto.IsDirect)
        {
            var targetUser = await _userManager.FindByIdAsync(dto.TargetUserId!);

            channel.Name = $"{currentUser.UserName}-{targetUser!.UserName}";
            channel.ChannelUsers = new List<ChannelUser>
            {
                new ChannelUser { User = currentUser, Channel = channel},
                new ChannelUser { User = targetUser, Channel = channel}
            };
        }
        else
        {
            channel.Name = dto.ChannelName!;
            channel.ChannelUsers = new List<ChannelUser>
            {
                new ChannelUser { User = currentUser, Channel = channel},
            };
        }

        await _channelRepository.CreateAsync(channel);
        await _channelRepository.SaveChangesAsync();

        return channel.Id.ToString();
    }

    public async Task ArchiveUnarchiveChannelAsync(string id)
    {
        var target = await _channelRepository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Channel>();
        target.IsArchived = target.IsArchived ? false : true;
        await _channelRepository.SaveChangesAsync();
    }

    public async Task DeleteChannelAsync(string id)
    {
        var target = await _channelRepository.GetByIdAsync(id);
        if (target == null) throw new NotFoundException<Channel>();
        await _channelRepository.DeleteAsync(target);
        await _channelRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Channel>> GetAllChannelsAsync()
    {
        return await _channelRepository.GetAllAsync(nameof(AppUser));
    }

    public async Task<Channel> GetChannelByIdAsync(string id)
    {
        var channel = await _channelRepository.GetByIdAsync(id, includes: nameof(Message));
        if (channel == null) throw new NotFoundException<Channel>();
        return channel;
    }
}