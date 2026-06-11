// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统异常日志服务接口
/// </summary>
public interface ISysLogExService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取异常日志分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysLogEx>> Page(PageExLogInput input);

    /// <summary>
    /// 获取异常日志详情
    /// </summary>
    Task<SysLogEx> GetDetail(long id);

    /// <summary>
    /// 清空异常日志
    /// </summary>
    void Clear();

    /// <summary>
    /// 导出异常日志
    /// </summary>
    Task<IActionResult> ExportLogEx(LogInput input);
}