// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 保存标注了JsonIgnore的Property的值信息
/// </summary>
public class JsonIgnoredPropertyData
{
    /// <summary>
    /// 记录索引
    /// </summary>
    public int RecordIndex { get; set; }

    /// <summary>
    /// 属性名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 属性值描述
    /// </summary>
    public string Value { get; set; }
}