// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

/// <summary>
/// 卡片公有数据
/// </summary>
public class DingTalkCardData
{
    /// <summary>
    /// 卡片模板内容替换参数，普通文本类型。
    /// </summary>
    public DingTalkCardParamMap CardParamMap { get; set; }

    /// <summary>
    /// 卡片模板内容替换参数，多媒体类型。
    /// </summary>
    public string CardMediaIdParamMap { get; set; }
}

/// <summary>
/// 卡片模板内容替换参数
/// </summary>
public class DingTalkCardParamMap
{
    /// <summary>
    /// 片模板内容替换参数
    /// </summary>
    [Newtonsoft.Json.JsonProperty("sys_full_json_obj")]
    [System.Text.Json.Serialization.JsonPropertyName("sys_full_json_obj")]
    public string SysFullJsonObj { get; set; }
}