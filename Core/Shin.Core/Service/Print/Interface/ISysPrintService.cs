// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统打印模板服务接口
/// </summary>
public interface ISysPrintService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取打印模板列表
    /// </summary>
    Task<SqlSugarPagedList<SysPrint>> Page(PagePrintInput input);

    /// <summary>
    /// 获取打印模板
    /// </summary>
    Task<SysPrint> GetPrint(string name);

    /// <summary>
    /// 增加打印模板
    /// </summary>
    Task AddPrint(AddPrintInput input);

    /// <summary>
    /// 更新打印模板
    /// </summary>
    Task UpdatePrint(UpdatePrintInput input);

    /// <summary>
    /// 删除打印模板
    /// </summary>
    Task DeletePrint(DeletePrintInput input);
}