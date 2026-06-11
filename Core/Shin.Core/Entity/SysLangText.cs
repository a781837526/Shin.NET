// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

[SugarTable(null, "翻译表")]
[SysTable]
[SugarIndex("index_{table}_N", nameof(EntityName), OrderByType.Asc)]
[SugarIndex("index_{table}_F", nameof(FieldName), OrderByType.Asc)]
public class SysLangText : EntityBase
{
    /// <summary>
    /// 所属实体名
    /// </summary>
    [SugarColumn(ColumnDescription = "所属实体名")]
    public string EntityName { get; set; }

    /// <summary>
    /// 所属实体ID
    /// </summary>
    [SugarColumn(ColumnDescription = "所属实体ID")]
    public long EntityId { get; set; }

    /// <summary>
    /// 字段名
    /// </summary>
    [SugarColumn(ColumnDescription = "字段名")]
    public string FieldName { get; set; }

    /// <summary>
    /// 语言代码
    /// </summary>
    [SugarColumn(ColumnDescription = "语言代码")]
    public string LangCode { get; set; }

    /// <summary>
    /// 多语言内容
    /// </summary>
    [SugarColumn(ColumnDescription = "翻译内容")]
    public string Content { get; set; }
}