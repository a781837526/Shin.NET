// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统OAuth服务接口
/// </summary>
public interface ISysOAuthService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 第三方登录 🔖
    /// </summary>
    Task<IActionResult> SignIn(string provider, string redirectUrl);

    /// <summary>
    /// 授权回调
    /// </summary>
    Task<IActionResult> SignInCallback(string provider = null, string redirectUrl = "");
}