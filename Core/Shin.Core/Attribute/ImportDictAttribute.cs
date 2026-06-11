// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 属性字典配置
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ImportDictAttribute : Attribute
{
    /// <summary>
    /// 字典Code
    /// </summary>
    public string TypeCode { get; set; }

    /// <summary>
    /// 目标对象名称
    /// </summary>
    public string TargetPropName { get; set; }
}