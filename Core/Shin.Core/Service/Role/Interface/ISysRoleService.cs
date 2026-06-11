// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统角色服务接口
/// </summary>
public interface ISysRoleService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取角色分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysRole>> Page(PageRoleInput input);

    /// <summary>
    /// 获取角色列表
    /// </summary>
    Task<List<RoleOutput>> GetList();

    /// <summary>
    /// 增加角色
    /// </summary>
    Task AddRole(AddRoleInput input);

    /// <summary>
    /// 更新角色
    /// </summary>
    Task UpdateRole(UpdateRoleInput input);

    /// <summary>
    /// 删除角色
    /// </summary>
    Task DeleteRole(DeleteRoleInput input);

    /// <summary>
    /// 授权角色菜单
    /// </summary>
    Task GrantMenu(RoleMenuInput input);

    /// <summary>
    /// 授权角色数据范围
    /// </summary>
    Task GrantDataScope(RoleOrgInput input);

    /// <summary>
    /// 根据角色Id获取菜单Id集合
    /// </summary>
    Task<List<long>> GetOwnMenuList(RoleInput input);

    /// <summary>
    /// 根据角色Id获取机构Id集合
    /// </summary>
    Task<List<long>> GetOwnOrgList(RoleInput input);

    /// <summary>
    /// 设置角色状态
    /// </summary>
    Task<int> SetStatus(RoleInput input);

    /// <summary>
    /// 删除与该角色相关的用户接口缓存
    /// </summary>
    Task ClearUserApiCache(long roleId);
}