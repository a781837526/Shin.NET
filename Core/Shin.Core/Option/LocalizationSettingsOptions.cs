// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 本地化选项配置
/// </summary>
public sealed class LocalizationSettingsOptions : IConfigurableOptions
{
    /// <summary>
    /// 语言列表
    /// </summary>
    public List<string> SupportedCultures { get; set; }

    /// <summary>
    /// 默认语言
    /// </summary>
    public string DefaultCulture { get; set; }

    /// <summary>
    /// 固定时间区域为特定时区（多语言）
    /// </summary>
    public string DateTimeFormatCulture { get; set; }
}