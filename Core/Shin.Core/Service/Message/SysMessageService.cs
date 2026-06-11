// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Microsoft.AspNetCore.SignalR;

namespace Shin.Core;

/// <summary>
/// 系统消息发送服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 370)]
public class SysMessageService : ISysMessageService
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 在线用户集线器
    /// </summary>
    private readonly IHubContext<OnlineUserHub, IOnlineUserHub> _chatHubContext;

    /// <summary>
    /// 初始化<see cref="SysMessageService"/>类的新实例
    /// </summary>
    public SysMessageService(ICacheManager cacheManager,
        IHubContext<OnlineUserHub, IOnlineUserHub> chatHubContext)
    {
        _cacheManager = cacheManager;
        _chatHubContext = chatHubContext;
    }

    /// <summary>
    /// 发送消息给所有人 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("发送消息给所有人")]
    public async Task SendAllUser(MessageInput input)
    {
        await _chatHubContext.Clients.All.ReceiveMessage(input);
    }

    /// <summary>
    /// 发送消息给除了发送人的其他人 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("发送消息给除了发送人的其他人")]
    public async Task SendOtherUser(MessageInput input)
    {
        var hashKey = _cacheManager.HashGetAll<SysOnlineUser>(CacheConst.KeyUserOnline);
        var exceptReceiveUsers = hashKey.Where(u => u.Value.UserId == input.ReceiveUserId).Select(u => u.Value).ToList();
        await _chatHubContext.Clients.AllExcept(exceptReceiveUsers.Select(t => t.ConnectionId)).ReceiveMessage(input);
    }

    /// <summary>
    /// 发送消息给某个人 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("发送消息给某个人")]
    public async Task SendUser(MessageInput input)
    {
        var hashKey = _cacheManager.HashGetAll<SysOnlineUser>(CacheConst.KeyUserOnline);
        var receiveUsers = hashKey.Where(u => u.Value.UserId == input.ReceiveUserId).Select(u => u.Value).ToList();
        await receiveUsers.ForEachAsync(u => _chatHubContext.Clients.Client(u.ConnectionId ?? "").ReceiveMessage(input));
    }

    /// <summary>
    /// 发送消息给某些人 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("发送消息给某些人")]
    public async Task SendUsers(MessageInput input)
    {
        var hashKey = _cacheManager.HashGetAll<SysOnlineUser>(CacheConst.KeyUserOnline);
        var receiveUsers = hashKey.Where(u => input.UserIds.Any(a => a == u.Value.UserId)).Select(u => u.Value).ToList();
        await receiveUsers.ForEachAsync(u => _chatHubContext.Clients.Client(u.ConnectionId ?? "").ReceiveMessage(input));
    }
}