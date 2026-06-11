// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统用户菜单快捷导航服务接口
/// </summary>
public interface ISysUserMenuService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 收藏菜单
    /// </summary>
    Task AddUserMenu(UserMenuInput input);

    /// <summary>
    /// 取消收藏菜单
    /// </summary>
    Task DeleteUserMenu(UserMenuInput input);

    /// <summary>
    /// 获取当前用户收藏的菜单集合
    /// </summary>
    Task<List<MenuOutput>> GetUserMenuList();

    /// <summary>
    /// 获取当前用户收藏的菜单Id集合
    /// </summary>
    Task<List<long>> GetUserMenuIdList();

    /// <summary>
    /// 删除指定用户的收藏菜单
    /// </summary>
    Task DeleteUserMenuList(long userId);

    /// <summary>
    /// 批量删除收藏菜单
    /// </summary>
    Task DeleteMenuList(List<long> ids);
}