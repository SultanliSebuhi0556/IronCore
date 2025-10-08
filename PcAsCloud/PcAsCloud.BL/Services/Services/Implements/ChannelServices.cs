using AutoMapper;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.BL.Exceptions.ChannelExceptions;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.CORE.RepositoryInstances;

namespace PcAsCloud.BL.Services.Services.Implements;
public class ChannelServices(IChannelRepository _channelRepository, IMapper _mapper) : IChannelServices
{

    public async Task<string> CreateChannelAsync(AppUser currentUser, bool isDirect, string? channelName, AppUser? targertUser)
    {
        var channel = new Channel()
        {
            IsDirect = isDirect
        };

        if (isDirect)
        {
            if (targertUser == null || string.IsNullOrWhiteSpace(channelName))
                throw new DirectChannelMustHaveTargetUserException();

            channel.Name = $"{currentUser.UserName}-{targertUser.UserName}";
            channel.Users = new List<AppUser> { currentUser, targertUser };
        }
        else
        {
            if (string.IsNullOrWhiteSpace(channelName))
                throw new IndirectChannelMustHaveChannelNameException();

            channel.Name = channelName!;
            channel.Users = new List<AppUser> { currentUser };
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

    public async Task<IEnumerable<ChannelGetDTO>> GetAllChannelsAsync()
    {
        var channels = await _channelRepository.GetAllAsync(nameof(AppUser));
        return _mapper.Map<IEnumerable<ChannelGetDTO>>(channels);
    }

    public async Task<ChannelGetDTO> GetChannelByIdAsync(string id)
    {
        var channel = await _channelRepository.GetByIdAsync(id);
        if (channel == null) throw new NotFoundException<Channel>();
        return _mapper.Map<ChannelGetDTO>(channel);
    }
}