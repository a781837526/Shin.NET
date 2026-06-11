// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 用户域账号服务接口
/// </summary>
public interface ISysUserLdapService : ITransient
{
    /// <summary>
    /// 批量插入域账号
    /// </summary>
    Task InsertUserLdapList(long tenantId, List<SysUserLdap> sysUserLdapList);

    /// <summary>
    /// 增加域账号
    /// </summary>
    Task AddUserLdap(long tenantId, long userId, string account, string domainAccount);

    /// <summary>
    /// 删除域账号
    /// </summary>
    Task DeleteUserLdapByUserId(long userId);
}