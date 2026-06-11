// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 翻译缓存服务接口
/// </summary>
public interface ISysLangTextCacheService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 【单条翻译获取】
    /// 根据实体类型、字段、主键ID 和语言编码获取翻译内容。<br/>
    /// 适用于：小表（如菜单、字典），可设置较长缓存时间。<br/>
    /// <br/>
    /// 【示例】<br/>
    /// var content = await _sysLangTextCacheService.GetTranslation("Product", "Name", 123, "en-US");
    /// </summary>
    /// <param name="entityName">实体名称，如 "Product"</param>
    /// <param name="fieldName">字段名称，如 "Name"</param>
    /// <param name="entityId">实体主键ID</param>
    /// <param name="langCode">语言编码，如 "zh-CN"</param>
    /// <returns>翻译后的内容（若无则返回 null 或空）</returns>
    Task<string> GetTranslation(string entityName, string fieldName, long entityId, string langCode);

    /// <summary>
    /// 根据实体类型、字段、主键ID 和语言编码获取翻译实体
    /// </summary>
    /// <param name="entityName">实体名称</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="entityId">实体主键ID</param>
    /// <param name="langCode">语言编码</param>
    Task<SysLangText> GetTranslationEntity(string entityName, string fieldName, long entityId, string langCode);

    /// <summary>
    /// 【批量翻译获取】<br/>
    /// 根据实体、字段和一批主键ID获取对应翻译内容，自动从缓存或数据库获取。<br/>
    /// 适用于：SKU、多商品、批量字典等需要高效批量获取的场景。<br/>
    ///
    /// 【示例】<br/>
    /// var dict = await _sysLangTextCacheService.GetTranslations("SKU", "Name", skuIds, "en_US");
    /// </summary>
    /// <param name="entityName">实体名称</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="entityIds">主键ID集合</param>
    /// <param name="langCode">语言编码</param>
    /// <returns>主键ID到翻译内容的字典</returns>
    Task<Dictionary<long, string>> GetTranslations(string entityName, string fieldName, List<long> entityIds, string langCode);

    /// <summary>
    /// 【列表翻译】<br/>
    /// 按配置把同一字段的翻译写回到实体列表中。内部会调用批量翻译接口。<br/>
    /// <br/>
    /// 【示例】<br/>
    /// await _sysLangTextCacheService.TranslateList(products, "Product", "Name", p =&gt; p.Id, (p, val) =&gt; p.Name = val, "zh-CN");
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="list">待翻译的实体列表</param>
    /// <param name="entityName">实体名称</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="idSelector">用于取出主键ID的表达式</param>
    /// <param name="setTranslatedValue">写回翻译值的委托</param>
    /// <param name="langCode">语言编码</param>
    /// <returns>翻译后的实体列表（引用传递）</returns>
    Task<List<TEntity>> TranslateList<TEntity>(List<TEntity> list, string entityName, string fieldName, Func<TEntity, long> idSelector, Action<TEntity, string> setTranslatedValue, string langCode);

    /// <summary>
    /// 【多字段批量翻译】
    /// 对列表中的实体对象，按配置的字段映射进行多字段翻译处理。<br/>
    /// 常用于：菜单多语言、商品多语言、SKU多语言等需要多字段翻译的场景。<br/><br/>
    /// ✅ 特点：<br/>
    /// 1️⃣ 可同时翻译同一实体的多个字段（如 Name、Description、Title 等）<br/>
    /// 2️⃣ 内部先尝试从缓存读取，如缓存未命中则批量查询数据库，并自动写回缓存<br/>
    /// 3️⃣ 引用传递，直接对原实体对象赋值，无需额外返回<br/><br/>
    /// 【使用示例】：<br/>
    /// <code>
    /// var fields = new List&lt;LangFieldMap&lt;Product&gt;&gt;
    /// {
    ///     new LangFieldMap&lt;Product&gt; {
    ///         EntityName = "Product",
    ///         FieldName = "Name",
    ///         IdSelector = p =&gt; p.Id,
    ///         SetTranslatedValue = (p, val) =&gt; p.Name = val
    ///     },
    ///     new LangFieldMap&lt;Product&gt; {
    ///         EntityName = "Product",
    ///         FieldName = "Description",
    ///         IdSelector = p =&gt; p.Id,
    ///         SetTranslatedValue = (p, val) =&gt; p.Description = val
    ///     }
    /// };
    /// await _sysLangTextCacheService.TranslateMultiFields(products, fields, "zh-CN");
    /// </code>
    /// </summary>
    /// <typeparam name="TEntity">要翻译的实体类型，如 Product/Menu/SKU 等</typeparam>
    /// <param name="list">需要翻译的实体对象列表</param>
    /// <param name="fields">需要翻译的字段映射集合，支持多个字段</param>
    /// <param name="langCode">语言编码，如 "zh-CN"、"en-US"、"it-IT" 等</param>
    /// <returns>翻译后的实体列表（引用传递，原对象已直接赋值）</returns>
    Task<List<TEntity>> TranslateMultiFields<TEntity>(List<TEntity> list, List<LangFieldMap<TEntity>> fields, string langCode);

    /// <summary>
    /// 删除缓存
    /// </summary>
    public void DeleteCache(string entityName, string fieldName, long entityId, string langCode);

    /// <summary>
    /// 更新缓存
    /// </summary>
    void UpdateCache(string entityName, string fieldName, long entityId, string langCode, string newValue);
}