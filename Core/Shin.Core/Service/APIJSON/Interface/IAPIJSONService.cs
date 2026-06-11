// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// APIJSON服务接口
/// </summary>
public interface IAPIJSONService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 统一查询入口
    /// </summary>
    /// <remarks>参数：{"[]":{"SYSLOGOP":{}}}</remarks>
    JObject Query(JObject jobject);

    /// <summary>
    /// 查询
    /// </summary>
    JObject QueryByTable(string table, JObject jobject);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="tables">表对象或数组，若没有传Id则后端生成Id</param>
    JObject Add(JObject tables);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="tables">支持多表、多Id批量更新</param>
    /// <remarks>只支持Id作为条件</remarks>
    JObject Edit(JObject tables);

    /// <summary>
    /// 删除
    /// </summary>
    /// <remarks>支持非Id条件、支持批量</remarks>
    JObject Delete(JObject tables);
}