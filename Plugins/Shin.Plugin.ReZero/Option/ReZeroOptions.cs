// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.ReZero;

public sealed class ReZeroOptions : IConfigurableOptions
{
    /// <summary>
    /// AccessTokenKey
    /// </summary>
    public string AccessTokenKey { get; set; }

    /// <summary>
    /// RefreshAccessTokenKey
    /// </summary>
    public string RefreshAccessTokenKey { get; set; }
}