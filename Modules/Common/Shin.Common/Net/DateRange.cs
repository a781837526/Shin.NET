// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common;

/// <summary>
/// 日期区间结构体
/// </summary>
public struct DateRange
{
    /// <summary>
    /// 区间起点
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 区间终点
    /// </summary>
    public DateTime? EndDate { get; set; }
}