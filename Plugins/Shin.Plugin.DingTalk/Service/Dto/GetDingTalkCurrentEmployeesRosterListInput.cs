// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class GetDingTalkCurrentEmployeesRosterListInput
{
    /// <summary>
    /// 员工的userId列表，多个userid之间使用逗号分隔，一次最多支持传100个值。
    /// </summary>
    [Newtonsoft.Json.JsonProperty("userid_list")]
    [System.Text.Json.Serialization.JsonPropertyName("userid_list")]
    public string UserIdList { get; set; }

    /// <summary>
    /// 需要获取的花名册字段field_code值列表，多个字段之间使用逗号分隔，一次最多支持传100个值。
    /// </summary>
    [Newtonsoft.Json.JsonProperty("field_filter_list")]
    [System.Text.Json.Serialization.JsonPropertyName("field_filter_list")]
    public string FieldFilterList { get; set; }

    /// <summary>
    /// 应用的AgentId
    /// </summary>
    [Newtonsoft.Json.JsonProperty("agentid")]
    [System.Text.Json.Serialization.JsonPropertyName("agentid")]
    public string AgentId { get; set; }
}