// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 打印类型枚举
/// </summary>
[Description("打印类型枚举")]
public enum PrintTypeEnum
{
    /// <summary>
    /// 浏览器打印
    /// </summary>
    [Description("浏览器打印")]
    Browser = 1,

    /// <summary>
    /// 浏览器打印
    /// </summary>
    [Description("客户端打印")]
    Client = 2,
}