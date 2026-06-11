// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统域登录配置服务接口
/// </summary>
public interface ISysLdapService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取系统域登录配置分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysLdap>> Page(SysLdapInput input);

    /// <summary>
    /// 增加系统域登录配置
    /// </summary>
    Task<long> Add(AddSysLdapInput input);

    /// <summary>
    /// 更新系统域登录配置
    /// </summary>
    Task Update(UpdateSysLdapInput input);

    /// <summary>
    /// 删除系统域登录配置
    /// </summary>
    Task Delete(DeleteSysLdapInput input);

    /// <summary>
    /// 获取系统域登录配置详情
    /// </summary>
    Task<SysLdap> GetDetail(DetailSysLdapInput input);

    /// <summary>
    /// 获取系统域登录配置列表 🔖
    /// </summary>
    Task<List<SysLdap>> GetList();

    /// <summary>
    /// 验证账号
    /// </summary>
    /// <param name="account">域用户</param>
    /// <param name="password">密码</param>
    /// <param name="tenantId">租户</param>
    Task<bool> AuthAccount(long? tenantId, string account, string password);

    /// <summary>
    /// 同步域用户
    /// </summary>
    Task<List<SysUserLdap>> SyncUserTenant(long tenantId);

    /// <summary>
    /// 同步域用户
    /// </summary>
    Task<List<SysUserLdap>> SyncUser(SyncSysLdapInput input);

    /// <summary>
    /// 同步域组织 🔖
    /// </summary>
    Task SyncDept(SyncSysLdapInput input);
}