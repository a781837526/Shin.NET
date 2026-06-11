// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统日程服务接口
/// </summary>
public interface ISysScheduleService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取日程列表
    /// </summary>
    Task<List<SysSchedule>> Page(ListScheduleInput input);

    /// <summary>
    /// 获取日程详情
    /// </summary>
    Task<SysSchedule> GetDetail(long id);

    /// <summary>
    /// 增加日程
    /// </summary>
    Task AddUserSchedule(AddScheduleInput input);

    /// <summary>
    /// 更新日程
    /// </summary>
    Task UpdateUserSchedule(UpdateScheduleInput input);

    /// <summary>
    /// 删除日程
    /// </summary>
    Task DeleteUserSchedule(DeleteScheduleInput input);

    /// <summary>
    /// 设置日程状态
    /// </summary>
    Task<int> SetStatus(ScheduleInput input);
}