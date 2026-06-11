// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统存储过程服务接口
/// </summary>
public interface ISysProcService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 导出存储过程数据
    /// </summary>
    /// <remarks>指定列，没有指定的字段会被隐藏</remarks>
    Task<IActionResult> PocExport2(ExportProcInput input);

    /// <summary>
    /// 根据模板导出存储过程数据
    /// </summary>
    Task<IActionResult> PocExport(ExportProcByTMPInput input);

    /// <summary>
    /// 获取存储过程返回表-Oracle、达梦参数顺序不能错 🔖
    /// </summary>
    Task<DataSet> ProcTable(BaseProcInput input);

    /// <summary>
    /// 获取存储过程返回数据集
    /// </summary>
    /// <remarks>Oracle、达梦参数顺序不能错。Oracle 返回table、table1，其他返回table1、table2。适用于报表、复杂详细页面等</remarks>
    Task<DataSet> CommonDataSet(BaseProcInput input);
}