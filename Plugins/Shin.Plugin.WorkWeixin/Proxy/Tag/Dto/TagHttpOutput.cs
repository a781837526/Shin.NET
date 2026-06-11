// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.WorkWeixin;

/// <summary>
/// 新增标签输出参数
/// </summary>
public class TagIdHttpOutput : BaseWorkOutput
{
    /// <summary>
    /// 标签Id
    /// </summary>
    [JsonProperty("tagid")]
    [JsonPropertyName("tagid")]
    public long? TagId { get; set; }
}

/// <summary>
/// 标签列表输出参数
/// </summary>
public class TagListHttpOutput : BaseWorkOutput
{
    /// <summary>
    /// 标签Id
    /// </summary>
    [JsonProperty("taglist")]
    [JsonPropertyName("taglist")]
    public List<TagHttpInput> TagList { get; set; }
}