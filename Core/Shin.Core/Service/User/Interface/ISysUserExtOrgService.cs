// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统用户扩展机构服务接口
/// </summary>
public interface ISysUserExtOrgService : ITransient
{
    /// <summary>
    /// 获取用户扩展机构集合
    /// </summary>
    Task<List<SysUserExtOrg>> GetUserExtOrgList(long userId);

    /// <summary>
    /// 更新用户扩展机构
    /// </summary>
    Task UpdateUserExtOrg(long userId, List<SysUserExtOrg> extOrgList);

    /// <summary>
    /// 根据机构Id集合删除扩展机构
    /// </summary>
    Task DeleteUserExtOrgByOrgIdList(List<long> orgIdList);

    /// <summary>
    /// 根据用户Id删除扩展机构
    /// </summary>
    Task DeleteUserExtOrgByUserId(long userId);

    /// <summary>
    /// 根据机构Id判断是否有用户
    /// </summary>
    Task<bool> HasUserOrg(long orgId);

    /// <summary>
    /// 根据职位Id判断是否有用户
    /// </summary>
    Task<bool> HasUserPos(long posId);
}