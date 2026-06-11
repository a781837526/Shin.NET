// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Furion.SpecificationDocument;

namespace Shin.Core.Interface;

/// <summary>
/// 系统登录授权服务接口
/// </summary>
public interface ISysAuthService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 账号密码登录
    /// </summary>
    Task<LoginOutput> Login(LoginInput input);

    /// <summary>
    /// 获取登录租户和用户
    /// </summary>
    Task<(SysTenant tenant, SysUser user)> GetLoginUserAndTenant(long? tenantId, string account = null, string phone = null);

    /// <summary>
    /// 验证锁屏密码
    /// </summary>
    Task<bool> UnLockScreen(string password);

    /// <summary>
    /// 手机号登录
    /// </summary>
    Task<LoginOutput> LoginPhone(LoginPhoneInput input);

    /// <summary>
    /// 获取登录账号
    /// </summary>
    Task<LoginUserOutput> GetUserInfo();

    /// <summary>
    /// 获取刷新Token
    /// </summary>
    /// <param name="accessToken">旧的AccessToken</param>
    /// <returns>新的AccessToken和RefreshToken</returns>
    Task<LoginOutput> GetRefreshToken(string accessToken);

    /// <summary>
    /// 退出系统
    /// </summary>
    Task Logout();

    /// <summary>
    /// 获取验证码
    /// </summary>
    dynamic GetCaptcha();

    /// <summary>
    /// 用户注册
    /// </summary>
    Task UserRegistration(UserRegistrationInput input);

    /// <summary>
    /// Swagger登录检查
    /// </summary>
    int SwaggerCheckUrl();

    /// <summary>
    /// Swagger登录提交
    /// </summary>
    Task<int> SwaggerSubmitUrl(SpecificationAuth auth);
}