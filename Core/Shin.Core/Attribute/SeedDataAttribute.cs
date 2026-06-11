// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 种子数据特性
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class SeedDataAttribute : Attribute
{
    /// <summary>
    /// 排序（越大越后执行）
    /// </summary>
    public int Order { get; set; } = 0;

    public SeedDataAttribute(int orderNo)
    {
        Order = orderNo;
    }
}