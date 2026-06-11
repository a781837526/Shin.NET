// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 全局分页查询输入参数
/// </summary>
public class BasePageInput : BaseFilter
{
    /// <summary>
    /// 当前页码
    /// </summary>
    [DataValidation(ValidationTypes.Numeric)]
    public virtual int Page { get; set; } = 1;

    /// <summary>
    /// 页码容量
    /// </summary>
    //[Range(0, 100, ErrorMessage = "页码容量超过最大限制")]
    [DataValidation(ValidationTypes.Numeric)]
    public virtual int PageSize { get; set; } = 20;

    /// <summary>
    /// 排序字段
    /// </summary>
    public virtual string Field { get; set; }

    /// <summary>
    /// 排序方向
    /// </summary>
    public virtual string Order { get; set; }

    /// <summary>
    /// 降序排序
    /// </summary>
    public virtual string DescStr { get; set; } = "descending";
}

/// <summary>
/// 全局分页查询输入参数(带时间)
/// </summary>
public class BasePageTimeInput : BasePageInput
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