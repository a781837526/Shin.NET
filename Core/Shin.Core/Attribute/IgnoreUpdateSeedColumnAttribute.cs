// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 忽略更新种子列特性（标记在实体属性）
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public class IgnoreUpdateSeedColumnAttribute : Attribute
{
}