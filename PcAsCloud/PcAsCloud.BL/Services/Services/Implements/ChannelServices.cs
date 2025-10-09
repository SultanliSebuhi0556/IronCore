using FluentValidation;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;

namespace PcAsCloud.BL.Services.Services.Implements;
public class ChannelServices(IChannelRepository _channelRepository, IValidator<ChannelCreateDTO> _validator) : IChannelServices
{

    public async Task<string> CreateChannelAsync(ChannelCreateDTO dto)
    {
        var channel = new Channel()
        {
            IsDirect = dto.IsDirect
        };

        await _validator.ValidateAndThrowAsync(dto);

        if (dto.IsDirect)
        {
            channel.Name = $"{dto.CurrentUser.UserName}-{dto.TargertUser!.UserName}";
            channel.ChannelUsers = new List<ChannelUser>
            {
                new ChannelUser { User = dto.CurrentUser, Channel = channel},
                new ChannelUser { User = dto.TargertUser, Channel = channel}
            };
        }
        else
        {
            channel.Name = dto.ChannelName!;
            channel.ChannelUsers = new List<ChannelUser>
            {
                new ChannelUser { User = dto.CurrentUser, Channel = channel},
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