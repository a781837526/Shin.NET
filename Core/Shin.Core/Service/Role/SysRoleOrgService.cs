// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统角色机构服务
/// </summary>
public class SysRoleOrgService : ISysRoleOrgService
{
    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysRoleOrg> _repository;

    /// <summary>
    /// 初始化<see cref="SysRoleOrgService"/>类的新实例
    /// </summary>
    public SysRoleOrgService(ISqlSugarRepository<SysRoleOrg> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 授权角色机构
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task GrantRoleOrg(RoleOrgInput input)
    {
        await _repository.DeleteAsync(u => u.RoleId == input.Id);
        if (input.DataScope == (int)DataScopeEnum.Define)
        {
            var roleOrgList = input.OrgIdList.Select(u => new SysRoleOrg
            {
                RoleId = input.Id,
                OrgId = u
            }).ToList();
            await _repository.InsertRangeAsync(roleOrgList);
        }
    }

    /// <summary>
    /// 根据角色Id集合获取角色机构Id集合
    /// </summary>
    /// <param name="roleIdList"></param>
    /// <returns></returns>
    public async Task<List<long>> GetRoleOrgIdList(List<long> roleIdList)
    {
        if (roleIdList?.Count > 0)
        {
            return await _repository.AsQueryable()
                .Where(u => roleIdList.Contains(u.RoleId))
                .Select(u => u.OrgId).ToListAsync();
        }
        else return new List<long>();
    }

    /// <summary>
    /// 根据机构Id集合删除角色机构
    /// </summary>
    /// <param name="orgIdList"></param>
    /// <returns></returns>
    public async Task DeleteRoleOrgByOrgIdList(List<long> orgIdList)
    {
        await _repository.DeleteAsync(u => orgIdList.Contains(u.OrgId));
    }

    /// <summary>
    /// 根据角色Id删除角色机构
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task DeleteRoleOrgByRoleId(long roleId)
    {
        await _repository.DeleteAsync(u => u.RoleId == roleId);
    }
}