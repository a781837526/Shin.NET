// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统角色机构服务接口
/// </summary>
public interface ISysRoleOrgService : ITransient
{
    /// <summary>
    /// 授权角色机构
    /// </summary>
    Task GrantRoleOrg(RoleOrgInput input);

    /// <summary>
    /// 根据角色Id集合获取角色机构Id集合
    /// </summary>
    Task<List<long>> GetRoleOrgIdList(List<long> roleIdList);

    /// <summary>
    /// 根据机构Id集合删除角色机构
    /// </summary>
    Task DeleteRoleOrgByOrgIdList(List<long> orgIdList);

    /// <summary>
    /// 根据角色Id删除角色机构
    /// </summary>
    Task DeleteRoleOrgByRoleId(long roleId);
}