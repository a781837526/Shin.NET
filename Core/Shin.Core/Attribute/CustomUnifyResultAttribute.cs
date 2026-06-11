// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 自定义规范化结果特性
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
public class CustomUnifyResultAttribute : Attribute
{
    public string Name { get; set; }

    public CustomUnifyResultAttribute(string name)
    {
        Name = name;
    }
}