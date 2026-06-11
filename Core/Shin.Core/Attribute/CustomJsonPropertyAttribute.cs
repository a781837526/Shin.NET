// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 自定义Json转换字段名
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class CustomJsonPropertyAttribute : Attribute
{
    public string Name { get; }

    public CustomJsonPropertyAttribute(string name)
    {
        Name = name;
    }
}