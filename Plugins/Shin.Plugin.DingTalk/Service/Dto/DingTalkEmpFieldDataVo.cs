// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class DingTalkEmpFieldDataVo
{
    /// <summary>
    /// 字段名称
    /// </summary>
    [Newtonsoft.Json.JsonProperty("field_name")]
    [System.Text.Json.Serialization.JsonPropertyName("field_name")]
    public string FieldName { get; set; }

    /// <summary>
    /// 字段标识
    /// </summary>
    [Newtonsoft.Json.JsonProperty("field_code")]
    [System.Text.Json.Serialization.JsonPropertyName("field_code")]
    public string FieldCode { get; set; }

    /// <summary>
    /// 分组标识
    /// </summary>
    [Newtonsoft.Json.JsonProperty("group_id")]
    [System.Text.Json.Serialization.JsonPropertyName("group_id")]
    public string GroupId { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Newtonsoft.Json.JsonProperty("field_value_list")]
    [System.Text.Json.Serialization.JsonPropertyName("field_value_list")]
    public List<DingTalkFieldValueVo> FieldValueList { get; set; }
}