// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.ApprovalFlow;

/// <summary>
/// 审批流流程记录
/// </summary>
[SugarTable(null, "审批流流程记录")]
public class ApprovalFlowRecord : EntityBaseOrg
{
    /// <summary>
    /// 表单名称
    /// </summary>
    [SugarColumn(ColumnDescription = "表单名称", Length = 255)]
    public string? FormName { get; set; }

    /// <summary>
    /// 表单状态
    /// </summary>
    [SugarColumn(ColumnDescription = "表单状态", Length = 32)]
    public string? FormStatus { get; set; }

    /// <summary>
    /// 表单触发
    /// </summary>
    [SugarColumn(ColumnDescription = "表单触发", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? FormJson { get; set; }

    /// <summary>
    /// 表单结果
    /// </summary>
    [SugarColumn(ColumnDescription = "表单结果", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? FormResult { get; set; }

    /// <summary>
    /// 流程结构
    /// </summary>
    [SugarColumn(ColumnDescription = "流程结构", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? FlowJson { get; set; }

    /// <summary>
    /// 流程结果
    /// </summary>
    [SugarColumn(ColumnDescription = "流程结果", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? FlowResult { get; set; }
}