// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统租户管理服务接口
/// </summary>
public interface ISysTenantService
{
    /// <summary>
    /// 获取租户分页列表 🔖
    /// </summary>
    Task<SqlSugarPagedList<TenantOutput>> Page(PageTenantInput input);

    /// <summary>
    /// 获取租户列表
    /// </summary>
    Task<dynamic> GetList();

    /// <summary>
    /// 获取当前租户系统信息
    /// </summary>
    Task<SysTenant> GetCurrentTenantSysInfo();

    /// <summary>
    /// 获取库隔离的租户列表
    /// </summary>
    Task<List<SysTenant>> GetTenantDbList();

    /// <summary>
    /// 增加租户
    /// </summary>
    Task AddTenant(AddTenantInput input);

    /// <summary>
    /// 设置logo
    /// </summary>
    void SetLogoUrl(SysTenant tenant, string logoBase64, string logoFileName);

    /// <summary>
    /// 设置租户状态
    /// </summary>
    Task<int> SetStatus(TenantInput input);

    /// <summary>
    /// 获取租户默认菜单
    /// </summary>
    /// <param name="ignoreHome">如果某租户需要定制主页，可以忽略</param>
    IEnumerable<SysTenantMenu> GetTenantDefaultMenuList(bool ignoreHome = false);

    /// <summary>
    /// 获取租户默认菜单
    /// </summary>
    IEnumerable<SysTenantMenu> GetBaseRoleMenuIdList();

    /// <summary>
    /// 删除租户
    /// </summary>
    Task DeleteTenant(DeleteTenantInput input);

    /// <summary>
    /// 更新租户
    /// </summary>
    Task UpdateTenant(UpdateTenantInput input);

    /// <summary>
    /// 授权租户菜单
    /// </summary>
    Task GrantMenu(TenantMenuInput input);

    /// <summary>
    /// 获取租户菜单Id集合
    /// </summary>
    Task<List<long>> GetTenantMenuList(BaseIdInput input);

    /// <summary>
    /// 重置租户管理员密码
    /// </summary>
    Task<string> ResetPwd(TenantUserInput input);

    /// <summary>
    /// 切换租户
    /// </summary>
    Task<LoginOutput> ChangeTenant(BaseIdInput input);

    /// <summary>
    /// 进入租管端
    /// </summary>
    Task<LoginOutput> GoTenant(BaseIdInput input);

    /// <summary>
    /// 同步授权菜单(用于版本更新后，同步授权数据)
    /// </summary>
    Task SyncGrantMenu(BaseIdInput input);

    /// <summary>
    /// 在非单用户登录模式下获取登录令牌
    /// </summary>
    Task<LoginOutput> GetAccessTokenInNotSingleLogin(SysUser user);

    /// <summary>
    /// 缓存所有租户
    /// </summary>
    Task CacheTenant(long tenantId = 0);

    /// <summary>
    /// 创建租户数据库
    /// </summary>
    Task CreateDb(TenantInput input);

    /// <summary>
    /// 获取租户下的用户列表
    /// </summary>
    Task<List<SysUser>> UserList(TenantIdInput input);

    /// <summary>
    /// 获取租户数据库连接
    /// </summary>
    SqlSugarScopeProvider GetTenantDbConnectionScope(long tenantId);
}