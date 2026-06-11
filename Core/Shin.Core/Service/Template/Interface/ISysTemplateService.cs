// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统消息模板服务接口
/// </summary>
public interface ISysTemplateService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取模板列表
    /// </summary>
    Task<SqlSugarPagedList<SysTemplate>> Page(PageTemplateInput input);

    /// <summary>
    /// 获取模板
    /// </summary>
    Task<SysTemplate> GetTemplate(string code);

    /// <summary>
    /// 预览模板内容
    /// </summary>
    Task<string> ProView(ProViewTemplateInput input);

    /// <summary>
    /// 增加模板
    /// </summary>
    Task AddTemplate(AddTemplateInput input);

    /// <summary>
    /// 更新模板
    /// </summary>
    Task UpdateTemplate(UpdateTemplateInput input);

    /// <summary>
    /// 删除模板
    /// </summary>
    Task DeleteTemplate(BaseIdInput input);

    /// <summary>
    /// 获取分组列表
    /// </summary>
    Task<List<string>> GetGroupList();

    /// <summary>
    /// 渲染模板内容
    /// </summary>
    Task<string> Render(RenderTemplateInput input);

    /// <summary>
    /// 渲染模板内容
    /// </summary>
    Task<string> RenderAsync(string content, object data);

    /// <summary>
    /// 根据编码渲染模板内容
    /// </summary>
    Task<string> RenderByCode(string code, Dictionary<string, object> data);
}