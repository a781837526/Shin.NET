// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
///
/// </summary>
public interface ISysLangService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 分页查询语言
    /// </summary>
    Task<SqlSugarPagedList<SysLangOutput>> Page(PageSysLangInput input);

    /// <summary>
    /// 获取语言详情
    /// </summary>
    Task<SysLang> Detail(QueryByIdSysLangInput input);

    /// <summary>
    /// 增加语言
    /// </summary>
    Task<long> Add(AddSysLangInput input);

    /// <summary>
    /// 更新语言
    /// </summary>
    Task Update(UpdateSysLangInput input);

    /// <summary>
    /// 删除语言
    /// </summary>
    Task Delete(DeleteSysLangInput input);

    /// <summary>
    /// 获取下拉列表数据
    /// </summary>
    Task<dynamic> DropdownData();
}