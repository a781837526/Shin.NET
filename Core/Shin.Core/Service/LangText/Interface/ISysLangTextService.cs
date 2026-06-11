// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 翻译服务接口
/// </summary>
public interface ISysLangTextService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 分页查询翻译表
    /// </summary>
    Task<SqlSugarPagedList<SysLangTextOutput>> Page(PageSysLangTextInput input);

    /// <summary>
    /// 获取翻译表
    /// </summary>
    Task<List<SysLangTextOutput>> List(ListSysLangTextInput input);

    /// <summary>
    /// 获取翻译表详情
    /// </summary>
    Task<SysLangText> Detail([FromQuery] QueryByIdSysLangTextInput input);

    /// <summary>
    /// 增加翻译表
    /// </summary>
    Task<long> Add(AddSysLangTextInput input);

    /// <summary>
    /// 更新翻译表
    /// </summary>
    Task Update(UpdateSysLangTextInput input);

    /// <summary>
    /// 删除翻译表
    /// </summary>
    Task Delete(DeleteSysLangTextInput input);

    /// <summary>
    /// 批量删除翻译表
    /// </summary>
    Task BatchDelete([Required(ErrorMessage = "主键列表不能为空")] List<DeleteSysLangTextInput> input);

    /// <summary>
    /// 批量保存翻译表
    /// </summary>
    void BatchSave([Required(ErrorMessage = "列表不能为空")] List<ImportSysLangTextInput> input);

    /// <summary>
    /// 导出翻译表记录
    /// </summary>
    Task<IActionResult> Export(PageSysLangTextInput input);

    /// <summary>
    /// 下载翻译表数据导入模板
    /// </summary>
    IActionResult DownloadTemplate();

    /// <summary>
    /// 导入翻译表记录
    /// </summary>
    IActionResult ImportData([Required] IFormFile file);

    /// <summary>
    /// DEEPSEEK 翻译接口
    /// </summary>
    Task<string> AiTranslateText(AiTranslateTextInput input);
}