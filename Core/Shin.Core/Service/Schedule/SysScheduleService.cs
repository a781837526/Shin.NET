// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统日程服务
/// </summary>
[ApiDescriptionSettings(Order = 295)]
public class SysScheduleService : ISysScheduleService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysSchedule> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 初始化<see cref="SysScheduleService"/>类的新实例
    /// </summary>
    public SysScheduleService(ISqlSugarRepository<SysSchedule> repository,
        IUserManager userManager)
    {
        _userManager = userManager;
        _repository = repository;
    }

    /// <summary>
    /// 获取日程列表
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取日程列表")]
    public async Task<List<SysSchedule>> Page(ListScheduleInput input)
    {
        return await _repository.AsQueryable()
            .Where(u => u.UserId == _userManager.UserId)
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.StartTime.ToString()), u => u.ScheduleTime >= input.StartTime)
            .WhereIF(!string.IsNullOrWhiteSpace(input.EndTime.ToString()), u => u.ScheduleTime <= input.EndTime)
            .OrderBy(u => u.StartTime, OrderByType.Asc)
            .ToListAsync();
    }

    /// <summary>
    /// 获取日程详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("获取日程详情")]
    public async Task<SysSchedule> GetDetail(long id)
    {
        return await _repository.GetFirstAsync(u => u.Id == id);
    }

    /// <summary>
    /// 增加日程
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加日程")]
    public async Task AddUserSchedule(AddScheduleInput input)
    {
        input.UserId = _userManager.UserId;
        await _repository.InsertAsync(input.Adapt<SysSchedule>());
    }

    /// <summary>
    /// 更新日程
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新日程")]
    public async Task UpdateUserSchedule(UpdateScheduleInput input)
    {
        await _repository.AsUpdateable(input.Adapt<SysSchedule>()).IgnoreColumns(true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除日程
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除日程")]
    public async Task DeleteUserSchedule(DeleteScheduleInput input)
    {
        await _repository.DeleteAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 设置日程状态
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("设置日程状态")]
    public async Task<int> SetStatus(ScheduleInput input)
    {
        if (!Enum.IsDefined(typeof(FinishStatusEnum), input.Status)) throw Oops.Oh(ErrorCodeEnum.D3005);

        return await _repository.AsUpdateable()
            .SetColumns(u => u.Status == input.Status)
            .Where(u => u.Id == input.Id)
            .ExecuteCommandAsync();
    }
}