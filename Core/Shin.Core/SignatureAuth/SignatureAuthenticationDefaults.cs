// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// Signature 身份验证处理程序相关的默认值
/// </summary>
public static class SignatureAuthenticationDefaults
{
    /// <summary>
    /// SignatureAuthenticationOptions.AuthenticationScheme 使用的默认值
    /// </summary>
    public const string AuthenticationScheme = "Signature";

    /// <summary>
    /// 附加在 HttpContext Item 中验证失败消息的 Key
    /// </summary>
    public const string AuthenticateFailMsgKey = "SignatureAuthenticateFailMsg";
}