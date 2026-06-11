// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统消息发送服务接口
/// </summary>
public interface ISysMessageService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 发送消息给所有人
    /// </summary>
    Task SendAllUser(MessageInput input);

    /// <summary>
    /// 发送消息给除了发送人的其他人
    /// </summary>
    Task SendOtherUser(MessageInput input);

    /// <summary>
    /// 发送消息给某个人
    /// </summary>
    Task SendUser(MessageInput input);

    /// <summary>
    /// 发送消息给某些人
    /// </summary>
    Task SendUsers(MessageInput input);
}