// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.WorkWeixin;

public class CreatAppChatOutput : BaseWorkOutput
{
    /// <summary>
    /// 群聊的唯一标志
    /// </summary>
    [JsonProperty("chatid")]
    [JsonPropertyName("chatid")]
    public string ChatId { get; set; }
}