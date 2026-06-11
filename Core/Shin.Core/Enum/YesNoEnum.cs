// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 是否枚举
/// </summary>
[Description("是否枚举")]
public enum YesNoEnum
{
    /// <summary>
    /// 是
    /// </summary>
    [Description("是"), Theme("success")]
    Y = 1,

    /// <summary>
    /// 否
    /// </summary>
    [Description("否"), Theme("danger")]
    N = 2
}