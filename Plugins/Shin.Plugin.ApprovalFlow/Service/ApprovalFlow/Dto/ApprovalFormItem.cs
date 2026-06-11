// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Shin.Plugin.ApprovalFlow;

public class ApprovalFormItem
{
    [JsonPropertyName("configId")]
    public string ConfigId { get; set; }

    [JsonPropertyName("tableName")]
    public string TableName { get; set; }

    [JsonPropertyName("entityName")]
    public string EntityName { get; set; }

    [JsonPropertyName("typeName")]
    public string TypeName { get; set; }

    [JsonPropertyName("route")]
    public string Route => EntityName[..1].ToLower() + EntityName[1..] + "/" + TypeName;
}