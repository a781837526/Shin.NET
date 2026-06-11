// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统用户配置参数服务接口
/// </summary>
public interface ISysUserConfigService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取配置参数分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysConfig>> Page(PageConfigInput input);

    /// <summary>
    /// 获取配置参数列表
    /// </summary>
    Task<List<SysConfig>> List(PageConfigInput input);

    /// <summary>
    /// 增加配置参数
    /// </summary>
    Task AddConfig(AddConfigInput input);

    /// <summary>
    /// 更新配置参数
    /// </summary>
    Task UpdateConfig(UpdateConfigInput input);

    /// <summary>
    /// 删除配置参数
    /// </summary>
    Task DeleteConfig(DeleteConfigInput input);

    /// <summary>
    /// 批量删除配置参数
    /// </summary>
    Task BatchDeleteConfig(List<long> ids);

    /// <summary>
    /// 获取配置参数详情
    /// </summary>
    Task<SysConfig> GetDetail(ConfigInput input);

    /// <summary>
    /// 根据Code获取配置参数
    /// </summary>
    Task<SysConfig> GetConfig(string code);

    /// <summary>
    /// 根据Code获取配置参数值
    /// </summary>
    /// <param name="code">编码</param>
    Task<string> GetConfigValueByCode(string code);

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <param name="code">编码</param>
    /// <param name="defaultValue">默认值</param>
    Task<string> GetConfigValueByCode(string code, string defaultValue = default);

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="code">编码</param>
    /// <param name="defaultValue">默认值</param>
    Task<T> GetConfigValueByCode<T>(string code, T defaultValue = default);

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="code">编码</param>
    /// <param name="userId">用户Id</param>
    /// <param name="defaultValue">默认值</param>
    Task<T> GetConfigValueByCode<T>(string code, long userId, T defaultValue = default);

    /// <summary>
    /// 更新配置参数值
    /// </summary>
    Task UpdateConfigValue(string code, string value);

    /// <summary>
    /// 获取分组列表
    /// </summary>
    Task<List<string>> GetGroupList();

    /// <summary>
    /// 批量更新配置参数值
    /// </summary>
    Task BatchUpdateConfig(List<BatchConfigInput> input);
}