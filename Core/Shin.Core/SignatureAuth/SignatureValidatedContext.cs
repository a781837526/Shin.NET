// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;

namespace Shin.Core;

/// <summary>
/// Signature 身份验证已验证上下文
/// </summary>
public class SignatureValidatedContext : ResultContext<SignatureAuthenticationOptions>
{
    public SignatureValidatedContext(HttpContext context,
        AuthenticationScheme scheme,
        SignatureAuthenticationOptions options)
        : base(context, scheme, options)
    {
    }

    /// <summary>
    /// 身份标识
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    /// 密钥
    /// </summary>
    public string AccessSecret { get; set; }
}