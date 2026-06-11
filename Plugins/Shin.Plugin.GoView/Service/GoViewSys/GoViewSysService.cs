// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Shin.Core.Interface;

namespace Shin.Plugin.GoView;

/// <summary>
/// 系统登录服务 🧩
/// </summary>
[UnifyProvider("GoView")]
[ApiDescriptionSettings(GoViewConst.GroupName, Module = "goview", Name = "sys", Order = 100, Description = "系统登录")]
public class GoViewSysService : IDynamicApiController
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysUser> _repository;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 系统登录授权服务
    /// </summary>
    private readonly ISysAuthService _sysAuthService;

    /// <summary>
    /// 初始化<see cref="GoViewSysService"/>类的新实例
    /// </summary>
    public GoViewSysService(ISqlSugarRepository<SysUser> repository,
        ICacheManager cacheManager,
        ISysAuthService sysAuthService)
    {
        _sysAuthService = sysAuthService;
        _repository = repository;
        _cacheManager = cacheManager;
    }

    /// <summary>
    /// GoView 登录 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("GoView 登录")]
    public async Task<GoViewLoginOutput> Login(GoViewLoginInput input)
    {
        // 设置默认租户
        input.TenantId ??= SqlSugarConst.DefaultTenantId;

        _cacheManager.Set($"{CacheConst.KeyConfig}{ConfigConst.SysCaptcha}", false);

        input.Password = CryptogramUtil.SM2Encrypt(input.Password);
        var loginResult = await _sysAuthService.Login(new LoginInput()
        {
            Account = input.Username,
            Password = input.Password,
        });

        _cacheManager.Remove($"{CacheConst.KeyConfig}{ConfigConst.SysCaptcha}");

        var sysUser = await _repository.AsQueryable().ClearFilter().FirstAsync(u => u.Account.Equals(input.Username));
        return new GoViewLoginOutput()
        {
            Userinfo = new GoViewLoginUserInfo
            {
                Id = sysUser.Id.ToString(),
                Username = sysUser.Account,
                Nickname = sysUser.NickName,
            },
            Token = new GoViewLoginToken
            {
                TokenValue = $"Bearer {loginResult.AccessToken}"
            }
        };
    }

    /// <summary>
    /// GoView 退出 🔖
    /// </summary>
    [DisplayName("GoView 退出")]
    public void GetLogout()
    {
        _sysAuthService.Logout();
    }

    /// <summary>
    /// 获取 OSS 上传接口 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [ApiDescriptionSettings(Name = "GetOssInfo")]
    [DisplayName("获取 OSS 上传接口")]
    public Task<GoViewOssUrlOutput> GetOssInfo()
    {
        return Task.FromResult(new GoViewOssUrlOutput { BucketURL = "" });
    }
}