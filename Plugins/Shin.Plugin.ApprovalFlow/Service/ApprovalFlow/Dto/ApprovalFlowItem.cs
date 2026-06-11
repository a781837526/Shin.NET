// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Shin.Plugin.ApprovalFlow;

public class ApprovalFlowItem
{
    [JsonPropertyName("nodes")]
    public List<ApprovalFlowNodeItem> Nodes { get; set; }

    [JsonPropertyName("edges")]
    public List<ApprovalFlowEdgeItem> Edges { get; set; }
}

public class ApprovalFlowNodeItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }

    [JsonPropertyName("properties")]
    public FlowProperties Properties { get; set; }

    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FlowTextItem Text { get; set; }
}

public class ApprovalFlowEdgeItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("sourceNodeId")]
    public string SourceNodeId { get; set; }

    [JsonPropertyName("targetNodeId")]
    public string TargetNodeId { get; set; }

    [JsonPropertyName("startPoint")]
    public FlowEdgePointItem StartPoint { get; set; }

    [JsonPropertyName("endPoint")]
    public FlowEdgePointItem EndPoint { get; set; }

    [JsonPropertyName("properties")]
    public FlowProperties Properties { get; set; }

    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FlowTextItem Text { get; set; }

    [JsonPropertyName("pointsList")]
    public List<FlowEdgePointItem> PointsList { get; set; }
}

public class FlowProperties
{
}

public class FlowTextItem
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class FlowEdgePointItem
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }
}