// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统角色菜单服务
/// </summary>
public class SysRoleMenuService : ISysRoleMenuService, ITransient
{
    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysRoleMenu> _repository;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 初始化<see cref="SysRoleMenuService"/>类的新实例
    /// </summary>
    public SysRoleMenuService(ISqlSugarRepository<SysRoleMenu> sysRoleMenuRep,
        ICacheManager cacheManager)
    {
        _repository = sysRoleMenuRep;
        _cacheManager = cacheManager;
    }

    /// <summary>
    /// 根据角色Id集合获取菜单Id集合
    /// </summary>
    /// <param name="roleIdList"></param>
    /// <returns></returns>
    public async Task<List<long>> GetRoleMenuIdList(List<long> roleIdList)
    {
        return await _repository.AsQueryable()
            .Where(u => roleIdList.Contains(u.RoleId))
            .Select(u => u.MenuId).ToListAsync();
    }

    /// <summary>
    /// 授权角色菜单 🔖
    /// </summary>
    [UnitOfWork]
    [DisplayName("授权角色菜单")]
    public async Task GrantRoleMenu(RoleMenuInput input)
    {
        await _repository.DeleteAsync(u => u.RoleId == input.Id);

        // 追加父级菜单
        var allIdList = await _repository.Context.Queryable<SysMenu>().Select(u => new { u.Id, u.Pid }).ToListAsync();
        var pIdList = allIdList.ToChildList(u => u.Pid, u => u.Id, u => input.MenuIdList.Contains(u.Id)).Select(u => u.Pid).Distinct().ToList();
        input.MenuIdList = input.MenuIdList.Concat(pIdList).Distinct().Where(u => u != 0).ToList();

        // 保存授权数据
        var menus = input.MenuIdList.Select(u => new SysRoleMenu
        {
            RoleId = input.Id,
            MenuId = u
        }).ToList();

        // 同步授权数据
        if (input.RoleIdList?.Count() > 0)
        {
            await _repository.DeleteAsync(u => input.RoleIdList.Contains(u.RoleId));
            input.RoleIdList.ForEach(u =>
            {
                menus.AddRange(input.MenuIdList.Select(v => new SysRoleMenu
                {
                    RoleId = u,
                    MenuId = v
                }));
            });
        }


        await _repository.InsertRangeAsync(menus);

        // 清除缓存
        // _cacheManager.RemoveByPrefixKey(CacheConst.KeyUserMenu);
        _cacheManager.RemoveByPrefixKey(CacheConst.KeyUserButton);
    }

    /// <summary>
    /// 根据菜单Id集合删除角色菜单
    /// </summary>
    /// <param name="menuIdList"></param>
    /// <returns></returns>
    public async Task DeleteRoleMenuByMenuIdList(List<long> menuIdList)
    {
        await _repository.DeleteAsync(u => menuIdList.Contains(u.MenuId));
    }

    /// <summary>
    /// 根据角色Id删除角色菜单
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task DeleteRoleMenuByRoleId(long roleId)
    {
        await _repository.DeleteAsync(u => u.RoleId == roleId);
    }
}