// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class LogVisOutput
{
    /// <summary>
    /// 登录地点
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 经度
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// 维度
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 日志时间
    /// </summary>
    public DateTime? LogDateTime { get; set; }
}