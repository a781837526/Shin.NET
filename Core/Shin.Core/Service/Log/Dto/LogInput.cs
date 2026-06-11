// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class PageOpLogInput : PageVisLogInput
{
    /// <summary>
    /// 模块名称
    /// </summary>
    public string? ControllerName { get; set; }
}

public class PageExLogInput : PageOpLogInput
{
}

public class PageVisLogInput : PageLogInput
{
    /// <summary>
    /// 方法名称
    ///</summary>
    public string? ActionName { get; set; }
}

public class PageLogInput : BasePageTimeInput
{
    /// <summary>
    /// 账号
    /// </summary>
    public string? Account { get; set; }

    /// <summary>
    /// 操作用时
    /// </summary>
    public long? Elapsed { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? RemoteIp { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }
}

public class LogInput
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}