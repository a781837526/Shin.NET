// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 忽略枚举类型转字典特性（标记在枚举类型）
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Enum, AllowMultiple = true, Inherited = true)]
public class IgnoreEnumToDictAttribute : Attribute
{
}