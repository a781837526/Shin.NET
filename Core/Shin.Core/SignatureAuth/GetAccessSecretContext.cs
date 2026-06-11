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
/// 获取 AccessKey 关联 AccessSecret 方法的上下文
/// </summary>
public class GetAccessSecretContext : BaseContext<SignatureAuthenticationOptions>
{
    public GetAccessSecretContext(HttpContext context,
        AuthenticationScheme scheme,
        SignatureAuthenticationOptions options)
        : base(context, scheme, options)
    {
    }

    /// <summary>
    /// 身份标识
    /// </summary>
    public string AccessKey { get; set; }
}