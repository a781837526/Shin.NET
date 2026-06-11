// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统角色菜单服务接口
/// </summary>
public interface ISysRoleMenuService
{
    /// <summary>
    /// 根据角色Id集合获取菜单Id集合
    /// </summary>
    Task<List<long>> GetRoleMenuIdList(List<long> roleIdList);

    /// <summary>
    /// 授权角色菜单
    /// </summary>
    Task GrantRoleMenu(RoleMenuInput input);

    /// <summary>
    /// 根据菜单Id集合删除角色菜单
    /// </summary>
    Task DeleteRoleMenuByMenuIdList(List<long> menuIdList);

    /// <summary>
    /// 根据角色Id删除角色菜单
    /// </summary>
    Task DeleteRoleMenuByRoleId(long roleId);
}