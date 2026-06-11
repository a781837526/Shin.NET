// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.WorkWeixin;

/// <summary>
/// 企业微信配置项
/// </summary>
public class WorkWeixinOptions : IConfigurableOptions
{
    /// <summary>
    /// 企业ID
    /// </summary>
    public string CorpId { get; set; }

    /// <summary>
    /// 企业微信凭证密钥
    /// </summary>
    public string CorpSecret { get; set; }
}