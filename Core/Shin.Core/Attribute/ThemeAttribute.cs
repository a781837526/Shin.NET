// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 枚举拓展主题样式
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class ThemeAttribute : Attribute
{
    public string Theme { get; private set; }

    public ThemeAttribute(string theme)
    {
        this.Theme = theme;
    }
}