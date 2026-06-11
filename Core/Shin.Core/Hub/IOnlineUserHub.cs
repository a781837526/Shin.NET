// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 在线用户集线器接口
/// </summary>
public interface IOnlineUserHub
{
    /// <summary>
    /// 在线用户列表
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task OnlineUserList(OnlineUserList context);

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ForceOffline(object context);

    /// <summary>
    /// 发布站内消息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task PublicNotice(SysNotice context);

    /// <summary>
    /// 接收消息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    Task ReceiveMessage(object context);
}