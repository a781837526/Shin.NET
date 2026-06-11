// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统差异日志服务接口
/// </summary>
public interface ISysLogDiffService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取差异日志分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysLogDiff>> Page(PageLogInput input);

    /// <summary>
    /// 获取差异日志详情
    /// </summary>
    Task<SysLogDiff> GetDetail(long id);
}