// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class LangFieldMap<TEntity>
{
    /// <summary>实体名，如 Product</summary>
    public string EntityName { get; set; }

    /// <summary>字段名，如 Name/Description</summary>
    public string FieldName { get; set; }

    /// <summary>如何取主键ID</summary>
    public Func<TEntity, long> IdSelector { get; set; }

    /// <summary>如何写回翻译值</summary>
    public Action<TEntity, string> SetTranslatedValue { get; set; }
}