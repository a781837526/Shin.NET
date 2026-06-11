// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 微信账号服务接口
/// </summary>
public interface ISysWechatUserService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取微信用户列表
    /// </summary>
    Task<SqlSugarPagedList<SysWechatUser>> Page(WechatUserInput input);

    /// <summary>
    /// 增加微信用户
    /// </summary>
    Task AddWechatUser(SysWechatUser input);

    /// <summary>
    /// 更新微信用户
    /// </summary>
    Task UpdateWechatUser(SysWechatUser input);

    /// <summary>
    /// 删除微信用户
    /// </summary>
    Task DeleteWechatUser(DeleteWechatUserInput input);
}