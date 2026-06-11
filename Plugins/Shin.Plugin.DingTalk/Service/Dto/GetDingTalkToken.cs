// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class GetDingTalkTokenOutput
{
    /// <summary>
    /// 生成的access_token
    /// </summary>
    [Newtonsoft.Json.JsonProperty("access_token")]
    [System.Text.Json.Serialization.JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// access_token的过期时间，单位秒
    /// </summary>
    [Newtonsoft.Json.JsonProperty("expires_in")]
    [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 返回码描述
    /// </summary>
    public string ErrMsg { get; set; }

    /// <summary>
    /// 返回码
    /// </summary>
    public int ErrCode { get; set; }
}