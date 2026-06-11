// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class ScheduleInput : BaseIdInput
{
    /// <summary>
    /// 状态
    /// </summary>
    public virtual FinishStatusEnum Status { get; set; }
}

public class ListScheduleInput
{
    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }
}

public class AddScheduleInput : SysSchedule
{
    /// <summary>
    /// 日程内容
    /// </summary>
    [Required(ErrorMessage = "日程内容不能为空")]
    public override string Content { get; set; }
}

public class UpdateScheduleInput : AddScheduleInput
{
}

public class DeleteScheduleInput : BaseIdInput
{
}