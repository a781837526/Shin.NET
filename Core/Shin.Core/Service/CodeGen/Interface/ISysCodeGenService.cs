// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统代码生成器服务接口
/// </summary>
public interface ISysCodeGenService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取代码生成分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysCodeGen>> Page(CodeGenInput input);

    /// <summary>
    /// 增加代码生成
    /// </summary>
    Task AddCodeGen(AddCodeGenInput input);

    /// <summary>
    /// 更新代码生成
    /// </summary>
    Task UpdateCodeGen(UpdateCodeGenInput input);

    /// <summary>
    /// 同步代码字段(保留历史作用类型)
    /// </summary>
    Task SyncCodeFieldGen(UpdateCodeGenInput input);

    /// <summary>
    /// 删除代码生成
    /// </summary>
    Task DeleteCodeGen(List<DeleteCodeGenInput> inputs);

    /// <summary>
    /// 获取代码生成详情
    /// </summary>
    Task<SysCodeGen> GetDetail(QueryCodeGenInput input);

    /// <summary>
    /// 获取数据库库集合
    /// </summary>
    Task<List<DatabaseOutput>> GetDatabaseList();

    /// <summary>
    /// 获取数据库表(实体)集合
    /// </summary>
    Task<List<TableOutput>> GetTableList(string configId = SqlSugarConst.MainConfigId);

    /// <summary>
    /// 根据表名获取列集合
    /// </summary>
    List<ColumnOuput> GetColumnListByTableName(string tableName, string configId = SqlSugarConst.MainConfigId);

    /// <summary>
    /// 获取程序保存位置
    /// </summary>
    List<string> GetApplicationNamespaces();

    /// <summary>
    /// 代码生成到本地
    /// </summary>
    Task<dynamic> RunLocal(SysCodeGen input);

    /// <summary>
    /// 获取代码生成预览
    /// </summary>
    Task<Dictionary<string, string>> Preview(SysCodeGen input);
}