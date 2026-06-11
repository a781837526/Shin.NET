// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统数据库管理服务接口
/// </summary>
public interface ISysDatabaseService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取库列表
    /// </summary>
    List<VisualDb> GetList();

    /// <summary>
    /// 获取可视化库表结构
    /// </summary>
    VisualDbTable GetVisualDbTable();

    /// <summary>
    /// 获取字段列表
    /// </summary>
    List<DbColumnOutput> GetColumnList(string tableName, string configId = SqlSugarConst.MainConfigId);

    /// <summary>
    /// 获取数据库数据类型列表 🔖
    /// </summary>
    List<string> GetDbTypeList(string configId = SqlSugarConst.MainConfigId);

    /// <summary>
    /// 增加列
    /// </summary>
    void AddColumn(DbColumnInput input);

    /// <summary>
    /// 删除列 🔖
    /// </summary>
    void DeleteColumn(DeleteDbColumnInput input);

    /// <summary>
    /// 编辑列
    /// </summary>
    void UpdateColumn(UpdateDbColumnInput input);

    /// <summary>
    /// 移动列位置
    /// </summary>
    void MoveColumn(MoveDbColumnInput input);

    /// <summary>
    /// 获取表列表
    /// </summary>
    List<DbTableInfo> GetTableList(string configId = SqlSugarConst.MainConfigId);

    /// <summary>
    /// 增加表
    /// </summary>
    void AddTable(DbTableInput input);

    /// <summary>
    /// 删除表
    /// </summary>
    void DeleteTable(DeleteDbTableInput input);

    /// <summary>
    /// 编辑表
    /// </summary>
    void UpdateTable(UpdateDbTableInput input);

    /// <summary>
    /// 创建实体
    /// </summary>
    void CreateEntity(CreateEntityInput input);

    /// <summary>
    /// 创建种子数据
    /// </summary>
    Task CreateSeedData(CreateSeedDataInput input);

    /// <summary>
    /// 备份数据库（PostgreSQL）
    /// </summary>
    Task<IActionResult> BackupDatabase();
}