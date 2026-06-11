// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 视图实体接口
/// </summary>
public interface ISqlSugarView
{
    /// <summary>
    /// 获取视图查询sql语句
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public string GetQueryableSqlString(SqlSugarScopeProvider db);
}