// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 枚举配置选项
/// </summary>
public sealed class EnumOptions : IConfigurableOptions
{
    /// <summary>
    /// 枚举实体程序集名称集合
    /// </summary>
    public List<string> EntityAssemblyNames { get; set; }
}