// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统操作日志服务接口
/// </summary>
public interface ISysLogOpService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取操作日志分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysLogOp>> Page(PageOpLogInput input);

    /// <summary>
    /// 获取操作日志详情
    /// </summary>
    Task<SysLogOp> GetDetail(long id);

    /// <summary>
    /// 清空操作日志
    /// </summary>
    void Clear();

    /// <summary>
    /// 导出操作日志
    /// </summary>
    Task<IActionResult> ExportLogOp(LogInput input);
}