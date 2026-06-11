// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统代码生成配置服务接口
/// </summary>
public interface ISysCodeGenConfigService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取代码生成配置列表
    /// </summary>
    Task<List<CodeGenConfig>> GetList(CodeGenConfig input);

    /// <summary>
    /// 更新代码生成配置
    /// </summary>
    Task UpdateCodeGenConfig(List<CodeGenConfig> inputList);

    /// <summary>
    /// 删除代码生成配置
    /// </summary>
    Task DeleteCodeGenConfig(long codeGenId);

    /// <summary>
    /// 获取代码生成配置详情
    /// </summary>
    Task<SysCodeGenConfig> GetDetail(CodeGenConfig input);

    /// <summary>
    /// 批量增加代码生成配置
    /// </summary>
    void AddList(List<ColumnOuput> tableColumnOutputList, SysCodeGen codeGenerate);

    /// <summary>
    /// 批量更新代码字段:先删除再新增，会保留历史字段操作类型
    /// </summary>
    Task UpdateList(List<ColumnOuput> tableColumnOutputList, long codeGenId);
}