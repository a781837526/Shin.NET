// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 登录类型枚举
/// </summary>
[Description("登录类型枚举")]
public enum LoginTypeEnum
{
    /// <summary>
    /// PC登录
    /// </summary>
    [Description("PC登录")]
    Login = 1,

    /// <summary>
    /// PC退出
    /// </summary>
    [Description("PC退出")]
    Logout = 2,

    /// <summary>
    /// PC注册
    /// </summary>
    [Description("PC注册")]
    Register = 3
}