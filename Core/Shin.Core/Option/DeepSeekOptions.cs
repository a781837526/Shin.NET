// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public sealed class DeepSeekOptions : IConfigurableOptions
{
    /// <summary>
    /// 源语言
    /// </summary>
    public string SourceLang { get; set; }

    /// <summary>
    /// Api地址
    /// </summary>
    public string ApiUrl { get; set; }

    /// <summary>
    /// API KEY
    /// </summary>
    public string ApiKey { get; set; }
}