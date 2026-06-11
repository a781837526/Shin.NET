// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 表唯一配置项
/// </summary>
public class TableUniqueConfigItem
{
    /// <summary>
    /// 字段列表
    /// </summary>
    public List<string> Columns { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 格式化查询条件
    /// </summary>
    /// <param name="separator">分隔符</param>
    /// <param name="format">模板字符串</param>
    /// <returns></returns>
    public string Format(string separator, string format) => string.Join(separator, Columns.Select(name => string.Format(format, name)));
}