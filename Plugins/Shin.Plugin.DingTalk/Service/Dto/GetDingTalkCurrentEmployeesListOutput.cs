// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class GetDingTalkCurrentEmployeesListOutput
{
    /// <summary>
    /// 查询到的员工userId列表
    /// </summary>
    [Newtonsoft.Json.JsonProperty("data_list")]
    [System.Text.Json.Serialization.JsonPropertyName("data_list")]
    public List<string> DataList { get; set; }

    /// <summary>
    /// 下一次分页调用的offset值，当返回结果里没有next_cursor时，表示分页结束。
    /// </summary>
    [Newtonsoft.Json.JsonProperty("next_cursor")]
    [System.Text.Json.Serialization.JsonPropertyName("next_cursor")]
    public int? NextCursor { get; set; }
}