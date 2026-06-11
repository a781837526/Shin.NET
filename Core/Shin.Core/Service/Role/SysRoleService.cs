// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统角色服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 480)]
public class SysRoleService : ISysRoleService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysRole> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 系统角色菜单服务
    /// </summary>
    private readonly ISysRoleMenuService _sysRoleMenuService;

    /// <summary>
    /// 系统用户角色服务
    /// </summary>
    private readonly ISysUserRoleService _sysUserRoleService;

    /// <summary>
    /// 系统角色机构服务
    /// </summary>
    private readonly ISysRoleOrgService _sysRoleOrgService;

    /// <summary>
    /// 系统菜单服务
    /// </summary>
    private readonly ISysMenuService _sysMenuService;

    /// <summary>
    /// 系统机构服务
    /// </summary>
    private readonly ISysOrgService _sysOrgService;

    /// <summary>
    /// 初始化<see cref="SysRoleService"/>类的新实例
    /// </summary>
    public SysRoleService(ISqlSugarRepository<SysRole> repository,
        IUserManager userManager,
        ISysOrgService sysOrgService,
        ISysMenuService sysMenuService,
        ISysRoleOrgService sysRoleOrgService,
        ISysRoleMenuService sysRoleMenuService,
        ISysUserRoleService sysUserRoleService,
        ICacheManager cacheManager)
    {
        _userManager = userManager;
        _repository = repository;
        _sysOrgService = sysOrgService;
        _sysMenuService = sysMenuService;
        _sysRoleOrgService = sysRoleOrgService;
        _sysRoleMenuService = sysRoleMenuService;
        _sysUserRoleService = sysUserRoleService;
        _cacheManager = cacheManager;
    }

    /// <summary>
    /// 获取角色分页列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取角色分页列表")]
    public async Task<SqlSugarPagedList<SysRole>> Page(PageRoleInput input)
    {
        // 当前用户已拥有的角色集合
        var roleIdList = _userManager.SuperAdmin ? new List<long>() : await _sysUserRoleService.GetUserRoleIdList(_userManager.UserId);
        return await _repository.AsQueryable()
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!_userManager.SuperAdmin, u => u.TenantId == _userManager.TenantId) // 若非超管，则只能操作本租户的角色
            .WhereIF(!_userManager.SuperAdmin && !_userManager.SysAdmin, u => u.CreateUserId == _userManager.UserId || roleIdList.Contains(u.Id)) // 若非超管且非系统管理员，则只能操作自己创建的角色|自己拥有的角色
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code))
            .OrderBy(u => new { u.OrderNo, u.Id })
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取角色列表 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取角色列表")]
    public async Task<List<RoleOutput>> GetList()
    {
        // 当前用户已拥有的角色集合
        var roleIdList = _userManager.SuperAdmin ? new List<long>() : await _sysUserRoleService.GetUserRoleIdList(_userManager.UserId);

        return await _repository.AsQueryable()
            .WhereIF(!_userManager.SuperAdmin, u => u.TenantId == _userManager.TenantId) // 若非超管，则只能操作本租户的角色
            .WhereIF(!_userManager.SuperAdmin && !_userManager.SysAdmin, u => u.CreateUserId == _userManager.UserId || roleIdList.Contains(u.Id)) // 若非超管且非系统管理员，则只显示自己创建和已拥有的角色
            .Where(u => u.Status != StatusEnum.Disable) // 非禁用的
            .OrderBy(u => new { u.OrderNo, u.Id }).Select<RoleOutput>().ToListAsync();
    }

    /// <summary>
    /// 增加角色 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加角色")]
    public async Task AddRole(AddRoleInput input)
    {
        if (await _repository.IsAnyAsync(u => u.Name == input.Name && u.Code == input.Code))
            throw Oops.Oh(ErrorCodeEnum.D1006);

        var newRole = await _repository.AsInsertable(input.Adapt<SysRole>()).ExecuteReturnEntityAsync();
        input.Id = newRole.Id;
        await UpdateRoleMenu(input);
    }

    /// <summary>
    /// 更新角色 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新角色")]
    public async Task UpdateRole(UpdateRoleInput input)
    {
        if (await _repository.IsAnyAsync(u => u.Name == input.Name && u.Code == input.Code && u.Id != input.Id))
            throw Oops.Oh(ErrorCodeEnum.D1006);

        await _repository.AsUpdateable(input.Adapt<SysRole>()).IgnoreColumns(true)
            .IgnoreColumns(u => new { u.DataScope }).ExecuteCommandAsync();

        await UpdateRoleMenu(input);
    }

    /// <summary>
    /// 删除角色 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除角色")]
    public async Task DeleteRole(DeleteRoleInput input)
    {
        // 若角色有用户则禁止删除
        var userIds = await _sysUserRoleService.GetUserIdList(input.Id);
        if (userIds != null && userIds.Count > 0) throw Oops.Oh(ErrorCodeEnum.D1025);

        // 若有绑定注册方案则禁止删除
        var hasUserRegWay = await _repository.Context.Queryable<SysUserRegWay>().AnyAsync(u => u.RoleId == input.Id);
        if (hasUserRegWay) throw Oops.Oh(ErrorCodeEnum.D1033);

        var sysRole = await _repository.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _repository.DeleteAsync(sysRole);

        // 级联删除角色机构数据
        await _sysRoleOrgService.DeleteRoleOrgByRoleId(sysRole.Id);

        // 级联删除用户角色数据
        await _sysUserRoleService.DeleteUserRoleByRoleId(sysRole.Id);

        // 级联删除角色菜单数据
        await _sysRoleMenuService.DeleteRoleMenuByRoleId(sysRole.Id);
    }

    /// <summary>
    /// 授权角色菜单 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("授权角色菜单")]
    public async Task GrantMenu(RoleMenuInput input)
    {
        if (input.MenuIdList == null || input.MenuIdList.Count < 1) return;

        await ClearUserApiCache(input.Id);

        await _sysRoleMenuService.GrantRoleMenu(input);
    }

    /// <summary>
    /// 授权角色数据范围 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("授权角色数据范围")]
    public async Task GrantDataScope(RoleOrgInput input)
    {
        // 删除与该角色相关的用户机构缓存
        var userIdList = await _sysUserRoleService.GetUserIdList(input.Id);
        foreach (var userId in userIdList)
        {
            SqlSugarFilter.DeleteUserOrgCache(userId, _repository.Context.CurrentConnectionConfig.ConfigId.ToString());
        }

        var role = await _repository.GetFirstAsync(u => u.Id == input.Id);
        var dataScope = input.DataScope;
        if (!_userManager.SuperAdmin)
        {
            switch (dataScope)
            {
                // 非超级管理员没有全部数据范围权限
                case (int)DataScopeEnum.All: throw Oops.Oh(ErrorCodeEnum.D1016);
                // 若数据范围自定义，则判断授权数据范围是否有权限
                case (int)DataScopeEnum.Define:
                    {
                        var grantOrgIdList = input.OrgIdList;
                        if (grantOrgIdList.Count > 0)
                        {
                            var orgIdList = await _sysOrgService.GetUserOrgIdList();
                            if (orgIdList.Count < 1)
                                throw Oops.Oh(ErrorCodeEnum.D1016);
                            if (!grantOrgIdList.All(u => orgIdList.Any(c => c == u)))
                                throw Oops.Oh(ErrorCodeEnum.D1016);
                        }

                        break;
                    }
            }
        }
        role.DataScope = (DataScopeEnum)dataScope;
        await _repository.AsUpdateable(role).UpdateColumns(u => new { u.DataScope }).ExecuteCommandAsync();
        await _sysRoleOrgService.GrantRoleOrg(input);
    }

    /// <summary>
    /// 根据角色Id获取菜单Id集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("根据角色Id获取菜单Id集合")]
    public async Task<List<long>> GetOwnMenuList([FromQuery] RoleInput input)
    {
        var menuIds = await _sysRoleMenuService.GetRoleMenuIdList(new List<long> { input.Id });
        return await _sysMenuService.ExcludeParentMenuOfFullySelected(menuIds);
    }

    /// <summary>
    /// 根据角色Id获取机构Id集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("根据角色Id获取机构Id集合")]
    public async Task<List<long>> GetOwnOrgList([FromQuery] RoleInput input)
    {
        return await _sysRoleOrgService.GetRoleOrgIdList(new List<long> { input.Id });
    }

    /// <summary>
    /// 设置角色状态 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("设置角色状态")]
    public async Task<int> SetStatus(RoleInput input)
    {
        if (!Enum.IsDefined(typeof(StatusEnum), input.Status)) throw Oops.Oh(ErrorCodeEnum.D3005);

        return await _repository.AsUpdateable()
            .SetColumns(u => u.Status == input.Status)
            .Where(u => u.Id == input.Id)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除与该角色相关的用户接口缓存
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [NonAction]
    public async Task ClearUserApiCache(long roleId)
    {
        var userIdList = await _sysUserRoleService.GetUserIdList(roleId);
        foreach (var userId in userIdList)
        {
            _cacheManager.Remove($"{CacheConst.KeyUserButton}{userId}");
        }
    }

    /// <summary>
    /// 更新角色菜单权限
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private async Task UpdateRoleMenu(AddRoleInput input)
    {
        if (input.MenuIdList == null || input.MenuIdList.Count < 1) return;
        await GrantMenu(new RoleMenuInput()
        {
            Id = input.Id,
            MenuIdList = input.MenuIdList.ToList()
        });
    }
}