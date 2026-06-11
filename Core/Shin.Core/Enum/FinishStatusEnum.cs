// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 完成状态枚举
/// </summary>
[Description("完成状态枚举")]
public enum FinishStatusEnum
{
    /// <summary>
    /// 已完成
    /// </summary>
    [Description("已完成"), Theme("success")]
    Finish = 1,

    /// <summary>
    /// 未完成
    /// </summary>
    [Description("未完成"), Theme("danger")]
    UnFinish = 0,
}