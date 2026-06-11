// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统在线用户服务接口
/// </summary>
public interface ISysOnlineUserService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysOnlineUser>> Page(PageOnlineUserInput input);

    /// <summary>
    /// 强制下线
    /// </summary>
    Task ForceOffline(SysOnlineUser user);

    /// <summary>
    /// 发布站内消息
    /// </summary>
    Task PublicNotice(SysNotice notice, List<long> userIds);

    /// <summary>
    /// 单用户登录
    /// </summary>
    Task SingleLogin(long userId);

    /// <summary>
    /// 通过用户ID踢掉在线用户
    /// </summary>
    Task ForceOffline(long userId);
}