// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统差异日志服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 330)]
public class SysLogDiffService : ISysLogDiffService
{
    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysLogDiff> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 初始化<see cref="SysLogDiffService"/>类的新实例
    /// </summary>
    public SysLogDiffService(IUserManager userManager, ISqlSugarRepository<SysLogDiff> repository)
    {
        _repository = repository;
        _userManager = userManager;
    }

    /// <summary>
    /// 获取差异日志分页列表 🔖
    /// </summary>
    /// <returns></returns>
    [SuppressMonitor]
    [DisplayName("获取差异日志分页列表")]
    public async Task<SqlSugarPagedList<SysLogDiff>> Page(PageLogInput input)
    {
        return await _repository.AsQueryable()
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.StartTime.ToString()), u => u.CreateTime >= input.StartTime)
            .WhereIF(!string.IsNullOrWhiteSpace(input.EndTime.ToString()), u => u.CreateTime <= input.EndTime)
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取差异日志详情 🔖
    /// </summary>
    /// <returns></returns>
    [SuppressMonitor]
    [DisplayName("获取差异日志详情")]
    public async Task<SysLogDiff> GetDetail(long id)
    {
        return await _repository.GetFirstAsync(u => u.Id == id);
    }
}