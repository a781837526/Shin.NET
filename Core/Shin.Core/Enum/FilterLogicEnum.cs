// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 过滤条件
/// </summary>
[Description("过滤条件")]
public enum FilterLogicEnum
{
    /// <summary>
    /// 并且
    /// </summary>
    [Description("并且")]
    And,

    /// <summary>
    /// 或者
    /// </summary>
    [Description("或者")]
    Or,

    /// <summary>
    /// 异或
    /// </summary>
    [Description("异或")]
    Xor
}