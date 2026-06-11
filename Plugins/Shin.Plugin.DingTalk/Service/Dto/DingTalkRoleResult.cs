// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class DingTalkRoleResult
{
    [JsonProperty("id")]
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public long id { get; set; }

    [JsonProperty("name")]
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string name { get; set; }
}