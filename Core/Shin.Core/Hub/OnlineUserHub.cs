// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Furion.InstantMessaging;
using Microsoft.AspNetCore.SignalR;

namespace Shin.Core;

/// <summary>
/// 在线用户集线器
/// </summary>
[MapHub("/hubs/onlineUser")]
public class OnlineUserHub : Hub<IOnlineUserHub>
{
    /// <summary>
    /// 租户分组前缀
    /// </summary>
    private const string GROUP_ONLINE = "GROUP_ONLINE_";

    /// <summary>
    /// 系统在线用户仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysOnlineUser> _repository;

    /// <summary>
    /// 系统消息发送服务
    /// </summary>
    private readonly ISysMessageService _sysMessageService;

    /// <summary>
    /// 在线用户集线器
    /// </summary>
    private readonly IHubContext<OnlineUserHub, IOnlineUserHub> _onlineUserHubContext;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 平台参数配置服务
    /// </summary>
    private readonly ISysConfigService _sysConfigService;

    /// <summary>
    /// 初始化<see cref="OnlineUserHub"/>类的新实例
    /// </summary>
    public OnlineUserHub(ISqlSugarRepository<SysOnlineUser> repository,
        ISysMessageService sysMessageService,
        IHubContext<OnlineUserHub, IOnlineUserHub> onlineUserHubContext,
        ICacheManager cacheManager,
        ISysConfigService sysConfigService)
    {
        _repository = repository;
        _sysMessageService = sysMessageService;
        _onlineUserHubContext = onlineUserHubContext;
        _cacheManager = cacheManager;
        _sysConfigService = sysConfigService;
    }

    /// <summary>
    /// 连接
    /// </summary>
    /// <returns></returns>
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var userId = (httpContext.User.FindFirst(ClaimConst.UserId)?.Value).ToLong();
        var account = httpContext.User.FindFirst(ClaimConst.Account)?.Value;
        var realName = httpContext.User.FindFirst(ClaimConst.RealName)?.Value;
        var tenantId = (httpContext.User.FindFirst(ClaimConst.TenantId)?.Value).ToLong();

        if (userId < 0 || string.IsNullOrWhiteSpace(account)) return;
        var user = new SysOnlineUser
        {
            ConnectionId = Context.ConnectionId,
            UserId = userId,
            UserName = account,
            RealName = realName,
            Time = DateTime.Now,
            Ip = httpContext.GetRemoteIpAddressToIPv4(true),
            Browser = httpContext.GetClientBrowser(),
            Os = httpContext.GetClientOs(),
            TenantId = tenantId,
        };
        await _repository.InsertAsync(user);

        // 是否开启单用户登录
        if (await _sysConfigService.GetConfigValue<bool>(ConfigConst.SysSingleLogin))
        {
            _cacheManager.HashAddOrUpdate(CacheConst.KeyUserOnline, "" + user.UserId, user);
        }
        else  // 非单用户登录则绑定用户连接Id
        {
            _cacheManager.HashAdd(CacheConst.KeyUserOnline, user.UserId + Context.ConnectionId, user);
        }

        // 以租户Id进行分组
        var groupName = $"{GROUP_ONLINE}{user.TenantId}";
        await _onlineUserHubContext.Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        var userList = await _repository.AsQueryable().Filter("", true)
            .Where(u => u.TenantId == user.TenantId).Take(10).ToListAsync();

        if (await _sysConfigService.GetConfigValue<bool>(ConfigConst.SysLoginOutReminder))
            await _onlineUserHubContext.Clients.Groups(groupName).OnlineUserList(new OnlineUserList
            {
                RealName = user.RealName,
                Online = true,
                UserList = userList
            });
    }

    /// <summary>
    /// 断开
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (string.IsNullOrEmpty(Context.ConnectionId)) return;

        var httpContext = Context.GetHttpContext();

        var user = await _repository.AsQueryable().Filter("", true).FirstAsync(u => u.ConnectionId == Context.ConnectionId);
        if (user == null) return;

        await _repository.DeleteByIdAsync(user.Id);

        // 是否开启单用户登录
        if (await _sysConfigService.GetConfigValue<bool>(ConfigConst.SysSingleLogin))
        {
            _cacheManager.HashDel<SysOnlineUser>(CacheConst.KeyUserOnline, "" + user.UserId);
            // _cacheManager.Remove(CacheConst.KeyUserOnline + user.UserId);
        }
        else
        {
            _cacheManager.HashDel<SysOnlineUser>(CacheConst.KeyUserOnline, user.UserId + Context.ConnectionId);
            // _cacheManager.Remove(CacheConst.KeyUserOnline + user.UserId + Context.ConnectionId);
        }

        // 通知当前组用户变动
        var userList = await _repository.AsQueryable().Filter("", true)
            .Where(u => u.TenantId == user.TenantId).Take(10).ToListAsync();

        if (await _sysConfigService.GetConfigValue<bool>(ConfigConst.SysLoginOutReminder))
            await _onlineUserHubContext.Clients.Groups($"{GROUP_ONLINE}{user.TenantId}").OnlineUserList(new OnlineUserList
            {
                RealName = user.RealName,
                Online = false,
                UserList = userList
            });
    }

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task ForceOffline(OnlineUserHubInput input)
    {
        await _onlineUserHubContext.Clients.Client(input.ConnectionId).ForceOffline("强制下线");
    }

    /// <summary>
    /// 发送信息给某个人
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task ClientsSendMessage(MessageInput message)
    {
        await _sysMessageService.SendUser(message);
    }

    /// <summary>
    /// 发送信息给所有人
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task ClientsSendMessageToAll(MessageInput message)
    {
        await _sysMessageService.SendAllUser(message);
    }

    /// <summary>
    /// 发送消息给某些人（除了本人）
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task ClientsSendMessageToOther(MessageInput message)
    {
        await _sysMessageService.SendOtherUser(message);
    }

    /// <summary>
    /// 发送消息给某些人
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task ClientsSendMessageToUsers(MessageInput message)
    {
        await _sysMessageService.SendUsers(message);
    }
}