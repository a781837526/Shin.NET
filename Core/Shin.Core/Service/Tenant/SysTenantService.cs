// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统租户管理服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 390)]
public class SysTenantService : ISysTenantService, IDynamicApiController, ITransient
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysTenant> _repository;

    /// <summary>
    /// 系统菜单服务
    /// </summary>
    private readonly ISysMenuService _sysMenuService;

    /// <summary>
    /// 系统角色菜单服务
    /// </summary>
    private readonly ISysRoleMenuService _sysRoleMenuService;

    /// <summary>
    /// 平台参数配置服务
    /// </summary>
    private readonly ISysConfigService _sysConfigService;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 文件上传配置选项
    /// </summary>
    private readonly UploadOptions _uploadOptions;

    /// <summary>
    /// 初始化<see cref="SysTenantService"/>类的新实例
    /// </summary>
    public SysTenantService(ISqlSugarRepository<SysTenant> repository,
        IOptions<UploadOptions> uploadOptions,
        ISysRoleMenuService sysRoleMenuService,
        ISysConfigService sysConfigService,
        ICacheManager cacheManager)
    {
        _repository = repository;
        _cacheManager = cacheManager;
        _uploadOptions = uploadOptions.Value;
        _sysConfigService = sysConfigService;
        _sysRoleMenuService = sysRoleMenuService;
    }

    /// <summary>
    /// 获取租户分页列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取租户分页列表")]
    public async Task<SqlSugarPagedList<TenantOutput>> Page(PageTenantInput input)
    {
        return await _repository.AsQueryable()
            .LeftJoin<SysUser>((u, a) => u.UserId == a.Id).ClearFilter()
            .LeftJoin<SysOrg>((u, a, b) => u.OrgId == b.Id).ClearFilter()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Phone), (u, a) => a.Phone.Contains(input.Phone.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), (u, a, b) => b.Name.Contains(input.Name.Trim()))
            .OrderBy(u => new { u.OrderNo, u.Id })
            .Select((u, a, b) => new TenantOutput
            {
                Id = u.Id,
                OrgId = b.Id,
                Name = b.Name,
                UserId = a.Id,
                AdminAccount = a.Account,
                Phone = a.Phone,
                Host = u.Host,
                Email = a.Email,
                TenantType = u.TenantType,
                DbType = u.DbType,
                Connection = u.Connection,
                ConfigId = u.ConfigId,
                OrderNo = u.OrderNo,
                Remark = u.Remark,
                Status = u.Status,
                CreateTime = u.CreateTime,
                CreateUserName = u.CreateUserName,
                UpdateTime = u.UpdateTime,
                UpdateUserName = u.UpdateUserName,
            }, true)
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取租户列表
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取租户列表"), HttpGet]
    public async Task<dynamic> GetList()
    {
        return await _repository.AsQueryable()
           .LeftJoin<SysOrg>((u, a) => u.OrgId == a.Id).ClearFilter()
           .Where(u => u.Status == StatusEnum.Enable)
           .Select((u, a) => new
           {
               Label = SqlFunc.HasValue(u.Title) ? $"{u.Title}-{a.Name}" : a.Name,
               Host = u.Host.ToLower(),
               Value = u.Id,
           }).ToListAsync();
    }

    /// <summary>
    /// 获取当前租户系统信息
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public async Task<SysTenant> GetCurrentTenantSysInfo()
    {
        var tenantId = long.Parse(App.User?.FindFirst(ClaimConst.TenantId)?.Value ?? "0");
        var host = App.HttpContext.Request.Host.Host.ToLower();
        var tenant = await _repository.AsQueryable()
            .WhereIF(tenantId > 0, u => u.Id == tenantId && SqlFunc.ToLower(u.Host).Contains(host))
            .WhereIF(!(tenantId > 0), u => SqlFunc.ToLower(u.Host).Contains(host))
            .FirstAsync();
        tenant ??= await _repository.GetFirstAsync(u => u.Id == SqlSugarConst.DefaultTenantId);
        _ = tenant ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        return tenant;
    }

    /// <summary>
    /// 获取库隔离的租户列表
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public async Task<List<SysTenant>> GetTenantDbList()
    {
        return await _repository.GetListAsync(u => u.TenantType == TenantTypeEnum.Db && u.Status == StatusEnum.Enable);
    }

    /// <summary>
    /// 增加租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加租户")]
    public async Task AddTenant(AddTenantInput input)
    {
        var isExist = await _repository.Context.Queryable<SysOrg>().AnyAsync(u => u.Name == input.Name);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1300);

        input.Host = input.Host?.ToLower();
        isExist = await _repository.IsAnyAsync(u => !string.IsNullOrWhiteSpace(u.Host) && u.Host == input.Host);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1303);

        isExist = await _repository.Context.Queryable<SysUser>().ClearFilter().AnyAsync(u => u.Account == input.AdminAccount);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1301);

        // 从库配置判断
        if (!string.IsNullOrWhiteSpace(input.SlaveConnections) && !JSON.IsValid(input.SlaveConnections)) throw Oops.Oh(ErrorCodeEnum.D1302);

        switch (input.TenantType)
        {
            // Id隔离时设置与主库一致
            case TenantTypeEnum.Id:
                var config = _repository.AsSugarClient().CurrentConnectionConfig;
                input.DbType = config.DbType;
                input.Connection = config.ConnectionString;
                break;

            case TenantTypeEnum.Db:
                if (string.IsNullOrWhiteSpace(input.Connection))
                    throw Oops.Oh(ErrorCodeEnum.Z1004);
                break;

            default:
                throw Oops.Oh(ErrorCodeEnum.D3004);
        }
        if (input.EnableReg == YesNoEnum.N) input.RegWayId = null;
        var tenant = input.Adapt<TenantOutput>();

        // 设置logo
        SetLogoUrl(tenant, input.LogoBase64, input.LogoFileName);

        tenant.Id = _repository.InsertReturnEntity(tenant).Id;
        await InitNewTenant(tenant);

        await CacheTenant();
    }

    /// <summary>
    /// 设置logo
    /// </summary>
    /// <param name="tenant"></param>
    /// <param name="logoBase64"></param>
    /// <param name="logoFileName"></param>
    [NonAction]
    public void SetLogoUrl(SysTenant tenant, string logoBase64, string logoFileName)
    {
        if (string.IsNullOrEmpty(tenant?.Logo) && string.IsNullOrEmpty(tenant?.Logo)) return;

        // 旧图标文件相对路径
        var oldSysLogoRelativeFilePath = tenant.Logo ?? "";
        var oldSysLogoAbsoluteFilePath = Path.Combine(App.WebHostEnvironment.WebRootPath, oldSysLogoRelativeFilePath.TrimStart('/'));

        var groups = Regex.Match(logoBase64, @"data:image/(?<type>.+?);base64,(?<data>.+)").Groups;

        //var type = groups["type"].Value;
        var base64Data = groups["data"].Value;
        var binData = Convert.FromBase64String(base64Data);

        // 根据文件名取扩展名
        var ext = string.IsNullOrWhiteSpace(logoFileName) ? ".png" : Path.GetExtension(logoFileName);

        // 本地图标保存路径
        var fileName = $"{tenant.ViceTitle}-logo{ext}".ToLower();
        var path = _uploadOptions.Path.Replace("/{yyyy}/{MM}/{dd}", "");
        path = path.StartsWith("/") || Regex.IsMatch(path, "^[A-Z|a-z]:") ? path : Path.Combine(App.WebHostEnvironment.WebRootPath, path);
        var absoluteFilePath = Path.Combine(path, fileName);

        // 删除已存在文件
        if (File.Exists(oldSysLogoAbsoluteFilePath)) File.Delete(oldSysLogoAbsoluteFilePath);

        // 创建文件夹
        var absoluteFileDir = Path.GetDirectoryName(absoluteFilePath);
        if (!Directory.Exists(absoluteFileDir)) Directory.CreateDirectory(absoluteFileDir);

        // 保存图标文件
        File.WriteAllBytesAsync(absoluteFilePath, binData);

        // 保存图标配置
        tenant.Logo = $"/upload/{fileName}";
    }

    /// <summary>
    /// 设置租户状态 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("设置租户状态")]
    public async Task<int> SetStatus(TenantInput input)
    {
        var tenant = await _repository.GetFirstAsync(u => u.Id == input.Id);
        if (tenant == null || tenant.ConfigId == SqlSugarConst.MainConfigId) throw Oops.Oh(ErrorCodeEnum.Z1001);

        if (!Enum.IsDefined(typeof(StatusEnum), input.Status)) throw Oops.Oh(ErrorCodeEnum.D3005);

        tenant.Status = input.Status;
        return await _repository.AsUpdateable(tenant).UpdateColumns(u => new { u.Status }).ExecuteCommandAsync();
    }

    /// <summary>
    /// 新增租户初始化
    /// </summary>
    /// <param name="tenant"></param>
    private async Task InitNewTenant(TenantOutput tenant)
    {
        var tenantId = tenant.Id;
        var tenantName = tenant.Name;

        // 初始化机构
        var newOrg = new SysOrg { TenantId = tenantId, Pid = 0, Name = tenantName, Code = tenantName, Remark = tenantName, };
        await _repository.Context.Insertable(newOrg).ExecuteCommandAsync();

        // 初始化默认角色
        var newRole = new SysRole { TenantId = tenantId, Name = CommonConst.DefaultBaseRoleName, Code = CommonConst.DefaultBaseRoleCode, DataScope = DataScopeEnum.Self, Remark = "此角色为系统自动创建角色" };
        var baseRole = await _repository.Context.Insertable(newRole).ExecuteReturnEntityAsync();
        var baseRoleMenuIdList = GetBaseRoleMenuIdList().ToList();
        await _sysRoleMenuService.GrantRoleMenu(new RoleMenuInput { Id = baseRole.Id, MenuIdList = baseRoleMenuIdList.Select(u => u.MenuId).ToList() });

        // 初始化职位
        var newPos = new SysPos { TenantId = tenantId, Name = "管理员-" + tenantName, Code = tenantName, Remark = tenantName };
        await _repository.Context.Insertable(newPos).ExecuteCommandAsync();

        // 初始化租户管理员账号
        var password = await _sysConfigService.GetConfigValue<string>(ConfigConst.SysPassword);
        var newUser = new SysUser
        {
            TenantId = tenantId,
            Account = tenant.AdminAccount,
            Password = CryptogramUtil.Encrypt(password),
            NickName = "系统管理员",
            Email = tenant.Email,
            Phone = tenant.Phone,
            AccountType = AccountTypeEnum.SysAdmin,
            OrgId = newOrg.Id,
            PosId = newPos.Id,
            Birthday = DateTime.Parse("2000-01-01"),
            RealName = "系统管理员",
            Remark = "系统管理员" + tenantName,
        };
        await _repository.Context.Insertable(newUser).ExecuteCommandAsync();

        // 关联租户组织机构和管理员用户
        await _repository.UpdateAsync(u => new SysTenant { UserId = newUser.Id, OrgId = newOrg.Id }, u => u.Id == tenantId);

        // 默认租户管理员角色菜单集合
        var menuList = GetTenantDefaultMenuList().ToList();
        await GrantMenu(new TenantMenuInput { Id = tenantId, MenuIdList = menuList.Select(u => u.MenuId).ToList() });
    }

    /// <summary>
    /// 获取租户默认菜单
    /// </summary>
    /// <param name="ignoreHome">如果某租户需要定制主页，可以忽略</param>
    /// <returns></returns>
    [NonAction]
    public IEnumerable<SysTenantMenu> GetTenantDefaultMenuList(bool ignoreHome = false)
    {
        var menuList = new List<SysMenu>();

        // 默认数据库配置
        var defaultConfig = App.GetOptions<DbConnectionOptions>().ConnectionConfigs.FirstOrDefault();
        //从程序集中获取种子菜单数据，种子菜单存在于其他类库中，需要动态加载
        var menuSeedDataTypeList = GetSeedDataTypes(defaultConfig, nameof(SysMenuSeedData));
        var allMenuList = new List<SysMenu>();
        foreach (var menu in menuSeedDataTypeList)
        {
            var menuSeedDataList = ((IEnumerable)menu.GetMethod("HasData")?.Invoke(Activator.CreateInstance(menu), null))?.Cast<SysMenu>();
            if (menuSeedDataList != null)
            {
                allMenuList.AddRange(menuSeedDataList);
            }
        }

        //实现三个层级的菜单
        var topMenuList = allMenuList.Where(u => u.Pid == 0 && u.Type == MenuTypeEnum.Dir).ToList();
        menuList.AddRange(topMenuList);

        var childMenuList = allMenuList.ToChildList(u => u.Id, u => u.Pid, u => topMenuList.Select(p => p.Id).Contains(u.Pid));
        menuList.AddRange(childMenuList);

        var endMenuList = allMenuList.ToChildList(u => u.Id, u => u.Pid, u => childMenuList.Select(p => p.Id).Contains(u.Pid));
        if (endMenuList != null)
        {
            menuList.AddRange(endMenuList);
        }
        //是否需要排除首页菜单
        if (ignoreHome) menuList = menuList.Where(u => !(u.Type == MenuTypeEnum.Menu && u.Name == "home")).ToList();

        menuList = menuList.Distinct().ToList();

        return menuList.Select(u => new SysTenantMenu
        {
            Id = CommonUtil.GetFixedHashCode("" + SqlSugarConst.DefaultTenantId + u.Id, 1300000000000),
            TenantId = SqlSugarConst.DefaultTenantId,
            MenuId = u.Id
        });
    }

    /// <summary>
    /// 获取种子数据类型
    /// </summary>
    /// <param name="config">数据库连接配置</param>
    /// <param name="typeName"></param>
    /// <returns>种子数据类型列表</returns>
    [NonAction]
    private List<Type> GetSeedDataTypes(DbConnectionConfig config, string typeName)
    {
        return App.EffectiveTypes
            .Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass && u.Name == typeName && u.GetInterfaces().Any(i => i.HasImplementedRawGeneric(typeof(ISqlSugarEntitySeedData<>))))
            .WhereIF(config.SeedSettings.EnableIncreSeed, u => u.IsDefined(typeof(IncreSeedAttribute), false))
            .OrderBy(u => u.GetCustomAttributes(typeof(SeedDataAttribute), false).Length > 0 ? ((SeedDataAttribute)u.GetCustomAttributes(typeof(SeedDataAttribute), false)[0]).Order : 0)
            .ToList();
    }

    /// <summary>
    /// 获取租户默认菜单
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public IEnumerable<SysTenantMenu> GetBaseRoleMenuIdList()
    {
        var menuList = new List<SysMenu>();
        var allMenuList = new SysMenuSeedData().HasData().ToList();

        var dashboardMenu = allMenuList.First(u => u.Type == MenuTypeEnum.Dir && u.Title == "工作台");
        menuList.AddRange(allMenuList.ToChildList(u => u.Id, u => u.Pid, dashboardMenu.Id));

        var systemMenu = allMenuList.First(u => u.Type == MenuTypeEnum.Dir && u.Title == "系统管理");
        menuList.Add(systemMenu);
        menuList.AddRange(allMenuList.ToChildList(u => u.Id, u => u.Pid, u => u.Pid == systemMenu.Id && new[] { "机构管理", "个人中心" }.Contains(u.Title)));
        menuList = menuList.Where(u => !new[] { "增加", "编辑", "删除" }.Contains(u.Title)).ToList();

        return menuList.Select(u => new SysTenantMenu
        {
            Id = CommonUtil.GetFixedHashCode("" + SqlSugarConst.DefaultTenantId + u.Id, 1300000000000),
            TenantId = SqlSugarConst.DefaultTenantId,
            MenuId = u.Id
        });
    }

    /// <summary>
    /// 删除租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除租户")]
    public async Task DeleteTenant(DeleteTenantInput input)
    {
        // 禁止删除默认租户
        if (input.Id.ToString() == SqlSugarConst.MainConfigId) throw Oops.Oh(ErrorCodeEnum.D1023);

        // 若账号为开放接口绑定租户则禁止删除
        var isOpenAccessTenant = await _repository.ChangeRepository<SqlSugarRepository<SysOpenAccess>>().IsAnyAsync(u => u.BindTenantId == input.Id);
        if (isOpenAccessTenant) throw Oops.Oh(ErrorCodeEnum.D1031);

        await _repository.DeleteAsync(u => u.Id == input.Id);

        await CacheTenant(input.Id);

        // 删除与租户相关的表数据
        await _repository.Context.Deleteable<SysTenantMenu>().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysTenantConfigData>().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();

        var users = await _repository.Context.Queryable<SysUser>().ClearFilter().Where(u => u.TenantId == input.Id).ToListAsync();
        var userIds = users.Select(u => u.Id).ToList();
        await _repository.Context.Deleteable<SysUser>().Where(u => userIds.Contains(u.Id)).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysUserRole>().Where(u => userIds.Contains(u.UserId)).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysUserExtOrg>().Where(u => userIds.Contains(u.UserId)).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysUserMenu>().Where(u => userIds.Contains(u.UserId)).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysUserConfigData>().Where(u => userIds.Contains(u.UserId)).ExecuteCommandAsync();

        var roleIds = await _repository.Context.Queryable<SysRole>().ClearFilter().Where(u => u.TenantId == input.Id).Select(u => u.Id).ToListAsync();
        await _repository.Context.Deleteable<SysRole>().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysRoleMenu>().Where(u => roleIds.Contains(u.RoleId)).ExecuteCommandAsync();
        await _repository.Context.Deleteable<SysRoleOrg>().Where(u => roleIds.Contains(u.RoleId)).ExecuteCommandAsync();

        await _repository.Context.Deleteable<SysOrg>().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();

        await _repository.Context.Deleteable<SysPos>().Where(u => u.TenantId == input.Id).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新租户")]
    public async Task UpdateTenant(UpdateTenantInput input)
    {
        var isExist = await _repository.Context.Queryable<SysOrg>().AnyAsync(u => u.Name == input.Name && u.Id != input.OrgId);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1300);

        input.Host = input.Host?.ToLower();
        isExist = await _repository.IsAnyAsync(u => !string.IsNullOrWhiteSpace(u.Host) && u.Host == input.Host && u.Id != input.Id);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1303);

        isExist = await _repository.Context.Queryable<SysUser>().ClearFilter().AnyAsync(u => u.Account == input.AdminAccount && u.Id != input.UserId);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1301);

        // Id隔离时设置与主库一致
        switch (input.TenantType)
        {
            case TenantTypeEnum.Id:
                var config = _repository.AsSugarClient().CurrentConnectionConfig;
                input.DbType = config.DbType;
                input.Connection = config.ConnectionString;
                break;

            case TenantTypeEnum.Db:
                if (string.IsNullOrWhiteSpace(input.Connection))
                    throw Oops.Oh(ErrorCodeEnum.Z1004);
                break;

            default:
                throw Oops.Oh(ErrorCodeEnum.D3004);
        }
        // 从库配置判断
        if (!string.IsNullOrWhiteSpace(input.SlaveConnections) && !JSON.IsValid(input.SlaveConnections))
            throw Oops.Oh(ErrorCodeEnum.D1302);

        // 设置logo
        var tenant = input.Adapt<SysTenant>();
        if (!string.IsNullOrWhiteSpace(input.LogoBase64)) SetLogoUrl(tenant, input.LogoBase64, input.LogoFileName);

        // 更新租户信息
        await _repository.AsUpdateable(tenant).IgnoreColumns(true).ExecuteCommandAsync();

        // 更新系统机构
        await _repository.Context.Updateable<SysOrg>(new SysOrg() { Name = input.Name }).Where(u => u.Id == input.OrgId).ExecuteCommandHasChangeAsync();

        // 更新系统用户
        await _repository.Context.Deleteable(new SysUser() { Account = input.AdminAccount, Phone = input.Phone, Email = input.Email }).Where(u => u.Id == input.UserId).ExecuteCommandHasChangeAsync();

        await CacheTenant(input.Id);
    }

    /// <summary>
    /// 授权租户菜单 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("授权租户菜单")]
    public async Task GrantMenu(TenantMenuInput input)
    {
        // 获取需要授权的菜单列表
        var menuList = await _repository.Context.Queryable<SysMenu>()
            .Where(u => input.MenuIdList.Contains(u.Id))
            .InnerJoin<SysTenantMenu>((u, t) => t.TenantId == input.Id && u.Id == t.MenuId)
            .ToListAsync();

        // 检查是否存在重复菜单
        if (menuList.Where(u => u.Type != MenuTypeEnum.Btn).GroupBy(u => new { u.Pid, u.Title }).Any(u => u.Count() > 1) ||
            menuList.Where(u => u.Type == MenuTypeEnum.Btn).GroupBy(u => u.Permission).Any(u => u.Count() > 1))
            throw Oops.Oh(ErrorCodeEnum.D1304);

        // 检查路由是否重复
        if (menuList.Where(u => !string.IsNullOrWhiteSpace(u.Name)).GroupBy(u => u.Name).Any(u => u.Count() > 1))
            throw Oops.Oh(ErrorCodeEnum.D4009);

        //获取默认租户授权菜单，种子数据主键ID保持不变，防止重复
        var tenantMenuList = input.Id == SqlSugarConst.DefaultTenantId ? await _repository.Context.Queryable<SysTenantMenu>().Where(u => u.TenantId == input.Id).ToListAsync() : null;

        List<long> tenantIdList = [input.Id];
        if (input.TenantIdList?.Count > 0) tenantIdList.AddRange(input.TenantIdList);

        // 删除旧记录
        await _repository.Context.Deleteable<SysTenantMenu>().Where(u => tenantIdList.Contains(u.TenantId)).ExecuteCommandAsync();

        // 追加父级菜单
        var allIdList = await _repository.Context.Queryable<SysMenu>().Select(u => new { u.Id, u.Pid }).ToListAsync();
        var pIdList = allIdList.ToChildList(u => u.Pid, u => u.Id, u => input.MenuIdList.Contains(u.Id)).Select(u => u.Pid).Distinct().ToList();
        input.MenuIdList = input.MenuIdList.Concat(pIdList).Distinct().Where(u => u != 0).ToList();

        // 保存租户菜单
        List<SysTenantMenu> sysTenantMenuList = new();
        tenantIdList.ForEach(tenantId =>
        {
            sysTenantMenuList.AddRange(input.MenuIdList.Select(menuId => new SysTenantMenu { TenantId = tenantId, MenuId = menuId }));
        });

        //默认租户授权菜单主键ID不变
        foreach (var item in sysTenantMenuList)
        {
            var tenantMenu = tenantMenuList.FirstOrDefault(u => u.TenantId == item.TenantId && u.MenuId == item.MenuId);
            if (tenantMenu != null) item.Id = tenantMenu.Id;
        }

        await _repository.Context.Insertable(sysTenantMenuList).ExecuteCommandAsync();

        // 清除菜单权限缓存
        _sysMenuService.DeleteMenuCache();
    }

    /// <summary>
    /// 获取租户菜单Id集合 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取租户菜单Id集合")]
    public async Task<List<long>> GetTenantMenuList([FromQuery] BaseIdInput input)
    {
        var menuIds = await _repository.Context.Queryable<SysTenantMenu>().Where(u => u.TenantId == input.Id).Select(u => u.MenuId).ToListAsync();
        return await _sysMenuService.ExcludeParentMenuOfFullySelected(menuIds);
    }

    /// <summary>
    /// 重置租户管理员密码 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("重置租户管理员密码")]
    public async Task<string> ResetPwd(TenantUserInput input)
    {
        var password = await _sysConfigService.GetConfigValue<string>(ConfigConst.SysPassword);
        var encryptPassword = CryptogramUtil.Encrypt(password);
        await _repository.Context.Updateable(new SysUser { Password = encryptPassword }).Where(u => u.Id == input.UserId).ExecuteCommandHasChangeAsync();
        return password;
    }

    /// <summary>
    /// 切换租户 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("切换租户")]
    public async Task<LoginOutput> ChangeTenant(BaseIdInput input)
    {
        var userId = (App.HttpContext?.User.FindFirst(ClaimConst.UserId)?.Value)?.ToLong();
        _ = await _repository.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        var user = await _repository.Context.Queryable<SysUser>().FirstAsync(u => u.Id == userId) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        user.TenantId = input.Id;

        return await GetAccessTokenInNotSingleLogin(user);
    }

    /// <summary>
    /// 进入租管端 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("进入租管端")]
    public async Task<LoginOutput> GoTenant(BaseIdInput input)
    {
        var tenant = await _repository.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        var user = await _repository.Context.Queryable<SysUser>().FirstAsync(u => u.Id == tenant.UserId) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        return await GetAccessTokenInNotSingleLogin(user);
    }

    /// <summary>
    /// 同步授权菜单(用于版本更新后，同步授权数据) 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("同步授权菜单")]
    public async Task SyncGrantMenu(BaseIdInput input)
    {
        var menuIdList = input.Id == SqlSugarConst.DefaultTenantId
            ? new SysMenuSeedData().HasData().Select(u => u.Id).ToList()
            : await _repository.Context.Queryable<SysRole>().ClearFilter()
              .InnerJoin<SysTenant>((u, t) => t.Id == input.Id && u.TenantId == t.Id)
              .InnerJoin<SysRoleMenu>((u, t, rm) => u.Id == rm.RoleId)
              .Select((u, t, rm) => rm.MenuId)
              .Distinct()
              .ToListAsync() ?? throw Oops.Oh(ErrorCodeEnum.D1019);
        var adminRole = await _repository.Context.Queryable<SysRole>().ClearFilter().FirstAsync(u => u.TenantId == input.Id && u.Code == "sys_admin");
        if (adminRole != null)
        {
            await _repository.Context.Deleteable<SysUserRole>().Where(u => u.RoleId == adminRole.Id).ExecuteCommandAsync();
            await App.GetService<SysRoleService>().DeleteRole(new DeleteRoleInput { Id = adminRole.Id });
        }
        await GrantMenu(new TenantMenuInput { Id = input.Id, MenuIdList = menuIdList });
    }

    /// <summary>
    /// 在非单用户登录模式下获取登录令牌
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [NonAction]
    public async Task<LoginOutput> GetAccessTokenInNotSingleLogin(SysUser user)
    {
        // 使用非单用户模式登录
        var singleLogin = _cacheManager.Get<bool>($"{CacheConst.KeyConfig}{ConfigConst.SysSingleLogin}");
        try
        {
            _cacheManager.Set($"{CacheConst.KeyConfig}{ConfigConst.SysSingleLogin}", false);
            return await App.GetService<SysAuthService>().CreateToken(user);
        }
        finally
        {
            // 恢复单用户登录参数
            if (singleLogin) _cacheManager.Set($"{CacheConst.KeyConfig}{ConfigConst.SysSingleLogin}", true);
        }
    }

    /// <summary>
    /// 缓存所有租户
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    [NonAction]
    public async Task CacheTenant(long tenantId = 0)
    {
        // 移除 ISqlSugarClient 中的库连接并排除默认主库
        if (tenantId > 0 && tenantId.ToString() != SqlSugarConst.MainConfigId)
            _repository.AsTenant().RemoveConnection(tenantId);

        var tenantList = await _repository.GetListAsync();

        // 对租户库连接进行SM2加密
        foreach (var tenant in tenantList.Where(tenant => !string.IsNullOrWhiteSpace(tenant.Connection)))
            tenant.Connection = CryptogramUtil.SM2Encrypt(tenant.Connection);

        _cacheManager.Set(CacheConst.KeyTenant, tenantList);
    }

    /// <summary>
    /// 创建租户数据库 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "CreateDb"), HttpPost]
    [DisplayName("创建租户数据库")]
    public async Task CreateDb(TenantInput input)
    {
        var tenant = await _repository.GetSingleAsync(u => u.Id == input.Id);
        if (tenant == null) return;

        if (tenant.DbType == SqlSugar.DbType.Oracle)
            throw Oops.Oh(ErrorCodeEnum.Z1002);

        if (string.IsNullOrWhiteSpace(tenant.Connection) || tenant.Connection.Length < 10)
            throw Oops.Oh(ErrorCodeEnum.Z1004);

        // 默认数据库配置
        var defaultConfig = App.GetOptions<DbConnectionOptions>().ConnectionConfigs.FirstOrDefault();

        var config = new DbConnectionConfig
        {
            ConfigId = tenant.Id.ToString(),
            DbType = tenant.DbType,
            ConnectionString = tenant.Connection,
            DbSettings = new DbSettings()
            {
                EnableInitDb = true,
                EnableDiffLog = false,
                EnableUnderLine = defaultConfig!.DbSettings.EnableUnderLine,
            }
        };
        SqlSugarSetup.InitTenantDatabase(App.GetRequiredService<ISqlSugarClient>().AsTenant(), config);
    }

    /// <summary>
    /// 获取租户下的用户列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取租户下的用户列表")]
    public async Task<List<SysUser>> UserList(TenantIdInput input)
    {
        return await _repository.Context.Queryable<SysUser>().ClearFilter().Where(u => u.TenantId == input.TenantId).ToListAsync();
    }

    /// <summary>
    /// 获取租户数据库连接
    /// </summary>
    /// <returns></returns>
    [NonAction]
    public SqlSugarScopeProvider GetTenantDbConnectionScope(long tenantId)
    {
        var iTenant = _repository.AsTenant();

        // 若已存在租户库连接，则直接返回
        if (iTenant.IsAnyConnection(tenantId.ToString())) return iTenant.GetConnectionScope(tenantId.ToString());

        lock (iTenant)
        {
            // 从缓存里面获取租户信息
            var tenant = _cacheManager.Get<List<SysTenant>>(CacheConst.KeyTenant)?.FirstOrDefault(u => u.Id == tenantId);
            if (tenant == null || tenant.TenantType == TenantTypeEnum.Id) return null;

            // 获取默认库连接配置
            var dbOptions = App.GetOptions<DbConnectionOptions>();
            var mainConnConfig = dbOptions.ConnectionConfigs.First(u => u.ConfigId.ToString() == SqlSugarConst.MainConfigId);

            // 设置租户库连接配置
            var tenantConnConfig = new DbConnectionConfig
            {
                ConfigId = tenant.Id.ToString(),
                DbType = tenant.DbType,
                TenantType = tenant.TenantType,
                IsAutoCloseConnection = true,
                ConnectionString = CryptogramUtil.SM2Decrypt(tenant.Connection), // 对租户库连接进行SM2解密
                DbSettings = new DbSettings()
                {
                    EnableUnderLine = mainConnConfig.DbSettings.EnableUnderLine,
                },
                SlaveConnectionConfigs = JSON.IsValid(tenant.SlaveConnections) ? JSON.Deserialize<List<SlaveConnectionConfig>>(tenant.SlaveConnections) : null // 从库连接配置
            };
            iTenant.AddConnection(tenantConnConfig);

            var sqlSugarScopeProvider = iTenant.GetConnectionScope(tenantId.ToString());
            SqlSugarSetup.SetDbConfig(tenantConnConfig);
            SqlSugarSetup.SetDbAop(sqlSugarScopeProvider, dbOptions.EnableConsoleSql, dbOptions.SuperAdminIgnoreIDeletedFilter);

            return sqlSugarScopeProvider;
        }
    }
}