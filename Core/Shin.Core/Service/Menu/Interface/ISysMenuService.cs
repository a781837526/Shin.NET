// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统菜单服务接口
/// </summary>
public interface ISysMenuService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取登录菜单树
    /// </summary>
    Task<List<MenuOutput>> GetLoginMenuTree();

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    Task<List<SysMenu>> GetList(MenuInput input);

    /// <summary>
    /// 增加菜单
    /// </summary>
    Task<long> AddMenu(AddMenuInput input);

    /// <summary>
    /// 更新菜单
    /// </summary>
    Task UpdateMenu(UpdateMenuInput input);

    /// <summary>
    /// 删除菜单
    /// </summary>
    Task DeleteMenu(DeleteMenuInput input);

    /// <summary>
    /// 设置菜单状态
    /// </summary>
    Task<int> SetStatus(MenuStatusInput input);

    /// <summary>
    /// 获取用户拥有按钮权限集合（缓存）
    /// </summary>
    Task<List<string>> GetOwnBtnPermList();

    /// <summary>
    /// 获取系统所有按钮权限集合（缓存）
    /// </summary>
    Task<List<string>> GetAllBtnPermList();

    /// <summary>
    /// 根据租户id获取构建菜单联表查询实例
    /// </summary>
    (ISugarQueryable<SysMenu, SysTenantMenu> query, long tenantId) GetSugarQueryableAndTenantId(long tenantId);

    /// <summary>
    /// 清除菜单和按钮缓存
    /// </summary>
    void DeleteMenuCache();

    /// <summary>
    /// 获取当前用户菜单Id集合
    /// </summary>
    Task<List<long>> GetMenuIdList();

    /// <summary>
    /// 排除前端存在全选的父级菜单
    /// </summary>
    Task<List<long>> ExcludeParentMenuOfFullySelected(List<long> menuIds);
}