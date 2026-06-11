// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 婚姻状况枚举
/// </summary>
[Description("婚姻状况枚举")]
public enum MaritalStatusEnum
{
    /// <summary>
    /// 未婚
    /// </summary>
    [Description("未婚")]
    UnMarried = 1,

    /// <summary>
    /// 已婚
    /// </summary>
    [Description("已婚")]
    Married = 2,

    /// <summary>
    /// 离异
    /// </summary>
    [Description("离异")]
    Divorce = 3,

    /// <summary>
    /// 再婚
    /// </summary>
    [Description("再婚")]
    Remarry = 4,

    /// <summary>
    /// 丧偶
    /// </summary>
    [Description("丧偶")]
    Widowed = 5,

    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")]
    None = 6,
}