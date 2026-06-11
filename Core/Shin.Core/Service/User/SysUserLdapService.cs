// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 用户域账号服务
/// </summary>
public class SysUserLdapService : ISysUserLdapService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysUserLdap> _repository;

    /// <summary>
    /// 初始化<see cref="SysUserLdapService"/>类的新实例
    /// </summary>
    public SysUserLdapService(ISqlSugarRepository<SysUserLdap> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 批量插入域账号
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="sysUserLdapList"></param>
    /// <returns></returns>
    public async Task InsertUserLdapList(long tenantId, List<SysUserLdap> sysUserLdapList)
    {
        await _repository.DeleteAsync(u => u.TenantId == tenantId);

        await _repository.InsertRangeAsync(sysUserLdapList);

        await _repository.AsUpdateable()
            .InnerJoin<SysUser>((l, u) => l.EmployeeId == u.Account)
            .SetColumns((l, u) => new SysUserLdap { UserId = u.Id })
            .Where((l, u) => l.TenantId == tenantId && u.Status == StatusEnum.Enable)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 增加域账号
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="account"></param>
    /// <param name="domainAccount"></param>
    /// <returns></returns>
    public async Task AddUserLdap(long tenantId, long userId, string account, string domainAccount)
    {
        var userLdap = await _repository.GetFirstAsync(u => u.TenantId == tenantId && (u.Account == account || u.UserId == userId || u.EmployeeId == domainAccount));
        if (userLdap != null) await _repository.DeleteByIdAsync(userLdap.Id);

        if (!string.IsNullOrWhiteSpace(domainAccount))
            await _repository.InsertAsync(new SysUserLdap { EmployeeId = account, TenantId = tenantId, UserId = userId, Account = domainAccount });
    }

    /// <summary>
    /// 删除域账号
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task DeleteUserLdapByUserId(long userId)
    {
        await _repository.DeleteAsync(u => u.UserId == userId);
    }
}