// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统用户扩展机构服务
/// </summary>
public class SysUserExtOrgService : ISysUserExtOrgService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysUserExtOrg> _repository;

    /// <summary>
    /// 初始化<see cref="SysUserExtOrgService"/>类的新实例
    /// </summary>
    public SysUserExtOrgService(ISqlSugarRepository<SysUserExtOrg> sysUserExtOrgRep)
    {
        _repository = sysUserExtOrgRep;
    }

    /// <summary>
    /// 获取用户扩展机构集合
    /// </summary>
    public async Task<List<SysUserExtOrg>> GetUserExtOrgList(long userId)
    {
        return await _repository.GetListAsync(u => u.UserId == userId);
    }

    /// <summary>
    /// 更新用户扩展机构
    /// </summary>
    public async Task UpdateUserExtOrg(long userId, List<SysUserExtOrg> extOrgList)
    {
        await _repository.DeleteAsync(u => u.UserId == userId);

        if (extOrgList == null || extOrgList.Count < 1) return;
        extOrgList.ForEach(u =>
        {
            u.UserId = userId;
        });
        await _repository.InsertRangeAsync(extOrgList);
    }

    /// <summary>
    /// 根据机构Id集合删除扩展机构
    /// </summary>
    public async Task DeleteUserExtOrgByOrgIdList(List<long> orgIdList)
    {
        await _repository.DeleteAsync(u => orgIdList.Contains(u.OrgId));
    }

    /// <summary>
    /// 根据用户Id删除扩展机构
    /// </summary>
    public async Task DeleteUserExtOrgByUserId(long userId)
    {
        await _repository.DeleteAsync(u => u.UserId == userId);
    }

    /// <summary>
    /// 根据机构Id判断是否有用户
    /// </summary>
    public async Task<bool> HasUserOrg(long orgId)
    {
        return await _repository.IsAnyAsync(u => u.OrgId == orgId);
    }

    /// <summary>
    /// 根据职位Id判断是否有用户
    /// </summary>
    public async Task<bool> HasUserPos(long posId)
    {
        return await _repository.IsAnyAsync(u => u.PosId == posId);
    }
}