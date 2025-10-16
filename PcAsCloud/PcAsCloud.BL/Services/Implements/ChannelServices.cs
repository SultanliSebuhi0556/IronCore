using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PcAsCloud.BL.DTOs.Channel;
using PcAsCloud.BL.Enums;
using PcAsCloud.BL.Exceptions.CommonExceptions;
using PcAsCloud.BL.Services.Instances;
using PcAsCloud.CORE.Entities;
using PcAsCloud.DAL.Context;

namespace PcAsCloud.BL.Services.Implements;
public class ChannelServices(
    AppDbContext _context,
    UserManager<AppUser> _userManager,
    IMapper _mapper,
    IHttpContextAccessor _httpContextAccessor,
    IValidator<ChannelCreateDTO> _validator) : IChannelServices
{

    public async Task JoinChannelAsync(string id)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var channel = await _context.Channels
            .Include(c => c.ChannelUsers)
            .FirstOrDefaultAsync(c => c.Id.ToString() == id);
        if (channel == null) throw new NotFoundException<Channel>();
        if (channel.IsDirect) throw new Exception("cant leave direct channels"); //TODO: ex

        if (channel.ChannelUsers.Any(x => x.AppUserId == user.Id)) throw new Exception("cant join this channel becouse u are init"); //TODO: ex

        channel.ChannelUsers.Add(new ChannelUser
        {
            AppUser = user,
            Channel = channel
        });
        await _context.SaveChangesAsync();
    }

    public async Task LeaveChannelAsync(string id)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var channel = await _context.Channels
            .Include(c => c.ChannelUsers)
            .FirstOrDefaultAsync(c => c.Id.ToString() == id);
        if (channel == null) throw new NotFoundException<Channel>();
        if (channel.IsDirect) throw new Exception("cant leave direct channels"); //TODO: ex

        var target = channel.ChannelUsers.FirstOrDefault(x => x.AppUserId == user.Id);
        if (target == null) throw new Exception("cant leave this channel becouse u are not init"); //TODO: ex

        channel.ChannelUsers.Remove(target);
        await _context.SaveChangesAsync();
    }

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
                new ChannelUser { AppUser = currentUser, Channel = channel},
                new ChannelUser { AppUser = targetUser, Channel = channel}
            };
        }
        else
        {
            channel.Name = dto.ChannelName!;
            channel.ChannelUsers = new List<ChannelUser>
            {
                new ChannelUser { AppUser = currentUser, Channel = channel},
            };
        }

        await _context.AddAsync(channel);
        await _context.SaveChangesAsync();

        return channel.Id.ToString();
    }

    public async Task ArchiveUnarchiveChannelAsync(string id)
    {
        var target = await _context.Channels.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (target == null) throw new NotFoundException<Channel>();

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        if (!target.ChannelUsers.Any(x => x.AppUser == user) || !rolesOfUser.Contains(nameof(UserRoles.Admin))) throw new Exception("cant delete this message "); //TODO: ex

        target.IsArchived = target.IsArchived ? false : true;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteChannelAsync(string id)
    {
        var target = await _context.Channels.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (target == null) throw new NotFoundException<Channel>();

        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (user == null) throw new NotFoundException<AppUser>();

        var rolesOfUser = await _userManager.GetRolesAsync(user);
        if (!target.ChannelUsers.Any(x => x.AppUser == user) || !rolesOfUser.Contains(nameof(UserRoles.Admin))) throw new Exception("cant delete this message "); //TODO: ex

        await Task.Run(() => _context.Remove(target));
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ChannelGetDTO>> GetAllChannelsAsync()
    {
        var channels = await _context.Channels
            .Include(x => x.ChannelUsers)
            .ThenInclude(x => x.AppUser)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ChannelGetDTO>>(channels);
    }

    public async Task<ChannelGetDTO> GetChannelByIdAsync(string id)
    {
        var channel = await _context.Channels
            .Include(x => x.ChannelUsers)
            .ThenInclude(x => x.AppUser)
            .FirstOrDefaultAsync(x => x.Id.ToString() == id);

        if (channel == null) throw new NotFoundException<Channel>();
        return _mapper.Map<ChannelGetDTO>(channel);
    }
}