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
/// Signature 身份验证扩展
/// </summary>
public static class SignatureAuthenticationExtensions
{
    /// <summary>
    /// 注册 Signature 身份验证处理模块
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddSignatureAuthentication(this AuthenticationBuilder builder)
    {
        return builder.AddSignatureAuthentication(options => { });
    }

    /// <summary>
    /// 注册 Signature 身份验证处理模块
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static AuthenticationBuilder AddSignatureAuthentication(this AuthenticationBuilder builder, Action<SignatureAuthenticationOptions> options)
    {
        return builder.AddScheme<SignatureAuthenticationOptions, SignatureAuthenticationHandler>(SignatureAuthenticationDefaults.AuthenticationScheme, options);
    }
}