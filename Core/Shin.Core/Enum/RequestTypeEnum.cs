// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// HTTP请求类型
/// </summary>
[Description("HTTP请求类型")]
public enum RequestTypeEnum
{
    /// <summary>
    /// 执行内部方法
    /// </summary>
    Run = 0,

    /// <summary>
    /// GET
    /// </summary>
    Get = 1,

    /// <summary>
    /// POST
    /// </summary>
    Post = 2,

    /// <summary>
    /// PUT
    /// </summary>
    Put = 3,

    /// <summary>
    /// DELETE
    /// </summary>
    Delete = 4
}