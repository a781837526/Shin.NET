// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 平台参数配置服务接口
/// </summary>
public interface ISysConfigService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取参数配置分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysConfig>> Page(PageConfigInput input);

    /// <summary>
    /// 获取参数配置列表
    /// </summary>
    Task<List<SysConfig>> List(PageConfigInput input);

    /// <summary>
    /// 增加参数配置
    /// </summary>
    Task AddConfig(AddConfigInput input);

    /// <summary>
    /// 更新参数配置
    /// </summary>
    Task UpdateConfig(UpdateConfigInput input);

    /// <summary>
    /// 删除参数配置
    /// </summary>
    Task DeleteConfig(DeleteConfigInput input);

    /// <summary>
    /// 批量删除参数配置
    /// </summary>
    Task BatchDeleteConfig(List<long> ids);

    /// <summary>
    /// 获取参数配置详情
    /// </summary>
    Task<SysConfig> GetDetail(ConfigInput input);

    /// <summary>
    /// 根据Code获取配置参数值
    /// </summary>
    Task<string> GetConfigValueByCode(string code);

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    Task<T> GetConfigValueByCode<T>(string code);

    /// <summary>
    /// 获取参数配置值
    /// </summary>
    Task<T> GetConfigValue<T>(string code);

    /// <summary>
    /// 更新参数配置值
    /// </summary>
    Task UpdateConfigValue(string code, string value);

    /// <summary>
    /// 获取分组列表
    /// </summary>
    Task<List<string>> GetGroupList();

    /// <summary>
    /// 获取 Token 过期时间
    /// </summary>
    Task<int> GetTokenExpire();

    /// <summary>
    /// 获取 RefreshToken 过期时间
    /// </summary>
    Task<int> GetRefreshTokenExpire();

    /// <summary>
    /// 批量更新参数配置值
    /// </summary>
    Task BatchUpdateConfig(List<BatchConfigInput> input);

    /// <summary>
    /// 获取系统信息
    /// </summary>
    Task<dynamic> GetSysInfo();

    /// <summary>
    /// 保存系统信息
    /// </summary>
    Task SaveSysInfo(InfoSaveInput input);
}