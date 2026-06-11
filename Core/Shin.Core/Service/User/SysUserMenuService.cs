// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统用户菜单快捷导航服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 445)]
public class SysUserMenuService : ISysUserMenuService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysUserMenu> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 初始化<see cref="SysUserMenuService"/>类的新实例
    /// </summary>
    public SysUserMenuService(ISqlSugarRepository<SysUserMenu> repository,
        IUserManager userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    /// <summary>
    /// 收藏菜单 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("收藏菜单")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task AddUserMenu(UserMenuInput input)
    {
        await _repository.DeleteAsync(u => u.UserId == _userManager.UserId);

        if (input.MenuIdList == null || input.MenuIdList.Count == 0) return;
        var menus = input.MenuIdList.Select(u => new SysUserMenu
        {
            UserId = _userManager.UserId,
            MenuId = u
        }).ToList();
        await _repository.InsertRangeAsync(menus);
    }

    /// <summary>
    /// 取消收藏菜单 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "DeleteUserMenu"), HttpPost]
    [DisplayName("取消收藏菜单")]
    public async Task DeleteUserMenu(UserMenuInput input)
    {
        await _repository.DeleteAsync(u => u.UserId == _userManager.UserId && input.MenuIdList.Contains(u.MenuId));
    }

    /// <summary>
    /// 获取当前用户收藏的菜单集合 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取当前用户收藏的菜单集合")]
    public async Task<List<MenuOutput>> GetUserMenuList()
    {
        var sysUserMenuList = await _repository.AsQueryable()
            .Includes(u => u.SysMenu)
            .Where(u => u.UserId == _userManager.UserId).ToListAsync();
        return sysUserMenuList.Where(u => u.SysMenu != null).Select(u => u.SysMenu).ToList().Adapt<List<MenuOutput>>();
    }

    /// <summary>
    /// 获取当前用户收藏的菜单Id集合 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取当前用户收藏的菜单Id集合")]
    public async Task<List<long>> GetUserMenuIdList()
    {
        return await _repository.AsQueryable()
            .Where(u => u.UserId == _userManager.UserId).Select(u => u.MenuId).ToListAsync();
    }

    /// <summary>
    /// 删除指定用户的收藏菜单
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public async Task DeleteUserMenuList(long userId)
    {
        await _repository.DeleteAsync(u => u.UserId == userId);
    }

    /// <summary>
    /// 批量删除收藏菜单
    /// </summary>
    /// <param name="ids"></param>
    [NonAction]
    public async Task DeleteMenuList(List<long> ids)
    {
        if (ids == null || ids.Count == 0) return;
        await _repository.DeleteAsync(u => ids.Contains(u.MenuId));
    }
}