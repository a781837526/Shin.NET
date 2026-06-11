// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class DingTalkWorkflowProcessInstancesOutput
{
    /// <summary>
    /// 请求Id
    /// </summary>
    [Newtonsoft.Json.JsonProperty("request_id")]
    [System.Text.Json.Serialization.JsonPropertyName("request_id")]
    public string RequestId { get; set; }

    public string code { get; set; }
    public string message { get; set; }

    /// <summary>
    /// 是否还有更多数据
    /// </summary>
    [JsonProperty("instanceId")]
    [System.Text.Json.Serialization.JsonPropertyName("instanceId")]
    public string instanceId { get; set; }
}