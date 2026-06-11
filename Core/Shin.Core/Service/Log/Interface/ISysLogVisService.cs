// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统访问日志服务接口
/// </summary>
public interface ISysLogVisService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取访问日志分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysLogVis>> Page(PageVisLogInput input);

    /// <summary>
    /// 清空访问日志
    /// </summary>
    void Clear();

    /// <summary>
    /// 获取访问日志列表
    /// </summary>
    Task<List<LogVisOutput>> GetList();
}