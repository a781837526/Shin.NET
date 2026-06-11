// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

[SugarTable("ding_talk_wokerflow_config", "审批配置表")]
public class DingTalkWokerflowConfig
{
    /// <summary>
    /// 审批名
    /// </summary>
    [SugarColumn(ColumnDescription = "审批名", IsPrimaryKey = true, IsIdentity = false)]
    public string WorkflowName { get; set; }

    /// <summary>
    /// 审批单Id
    /// </summary>
    [SugarColumn(ColumnDescription = "审批单Id")]
    public string ProcessCode { get; set; }
}