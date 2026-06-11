// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 消息类型枚举
/// </summary>
[Description("消息类型枚举")]
public enum MessageTypeEnum
{
    /// <summary>
    /// 普通信息
    /// </summary>
    [Description("消息"), Theme("info")]
    Info = 0,

    /// <summary>
    /// 成功提示
    /// </summary>
    [Description("成功"), Theme("success")]
    Success = 1,

    /// <summary>
    /// 警告提示
    /// </summary>
    [Description("警告"), Theme("warning")]
    Warning = 2,

    /// <summary>
    /// 错误提示
    /// </summary>
    [Description("错误"), Theme("danger")]
    Error = 3
}