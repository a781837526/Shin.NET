// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统存储过程服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 102)]
public class SysProcService : ISysProcService
{
    /// <summary>
    /// SqlSugar客户端
    /// </summary>
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 初始化<see cref="SysProcService"/>类的新实例
    /// </summary>
    /// <param name="db"></param>
    public SysProcService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 导出存储过程数据 🔖
    /// </summary>
    /// <remarks>指定列，没有指定的字段会被隐藏</remarks>
    public async Task<IActionResult> PocExport2(ExportProcInput input)
    {
        var db = _db.AsTenant().GetConnectionScope(input.ConfigId);
        var dt = await db.Ado.UseStoredProcedure().GetDataTableAsync(input.ProcId, input.ProcParams);

        var headers = new Dictionary<string, Tuple<string, int>>();
        var index = 1;
        foreach (var val in input.EHeader)
        {
            headers.Add(val.Key.ToUpper(), new Tuple<string, int>(val.Value, index));
            index++;
        }
        var excelExporter = new ExcelExporter();
        var da = await excelExporter.ExportAsByteArray(dt, new ProcExporterHeaderFilter(headers));
        return new FileContentResult(da, "application/octet-stream") { FileDownloadName = input.ProcId + ".xlsx" };
    }

    /// <summary>
    /// 根据模板导出存储过程数据 🔖
    /// </summary>
    public async Task<IActionResult> PocExport(ExportProcByTMPInput input)
    {
        var db = _db.AsTenant().GetConnectionScope(input.ConfigId);
        var dt = await db.Ado.UseStoredProcedure().GetDataTableAsync(input.ProcId, input.ProcParams);

        var excelExporter = new ExcelExporter();
        string template = AppDomain.CurrentDomain.BaseDirectory + "/wwwroot/template/" + input.Template + ".xlsx";
        var bs = await excelExporter.ExportBytesByTemplate(dt, template);
        return new FileContentResult(bs, "application/octet-stream") { FileDownloadName = input.ProcId + ".xlsx" };
    }

    /// <summary>
    /// 获取存储过程返回表-Oracle、达梦参数顺序不能错 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<DataSet> ProcTable(BaseProcInput input)
    {
        var db = _db.AsTenant().GetConnectionScope(input.ConfigId);
        var dt = await db.Ado.UseStoredProcedure().GetDataTableAsync(input.ProcId, input.ProcParams);
        return new DataSet { Tables = { dt } };
    }

    /// <summary>
    /// 获取存储过程返回数据集 🔖
    /// </summary>
    /// <remarks>Oracle、达梦参数顺序不能错。Oracle 返回table、table1，其他返回table1、table2。适用于报表、复杂详细页面等</remarks>
    [HttpPost]
    public async Task<DataSet> CommonDataSet(BaseProcInput input)
    {
        var db = _db.AsTenant().GetConnectionScope(input.ConfigId);
        return await db.Ado.UseStoredProcedure().GetDataSetAllAsync(input.ProcId, input.ProcParams);
    }
}