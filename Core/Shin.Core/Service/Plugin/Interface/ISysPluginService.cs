// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统动态插件服务接口
/// </summary>
public interface ISysPluginService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取动态插件列表
    /// </summary>
    Task<SqlSugarPagedList<SysPlugin>> Page(PagePluginInput input);

    /// <summary>
    /// 增加动态插件
    /// </summary>
    Task AddPlugin(AddPluginInput input);

    /// <summary>
    /// 更新动态插件
    /// </summary>
    Task UpdatePlugin(UpdatePluginInput input);

    /// <summary>
    /// 删除动态插件
    /// </summary>
    Task DeletePlugin(DeletePluginInput input);

    /// <summary>
    /// 添加动态程序集/接口
    /// </summary>
    /// <param name="csharpCode"></param>
    /// <param name="assemblyName">程序集名称</param>
    string CompileAssembly(string csharpCode, string assemblyName = default);

    /// <summary>
    /// 移除动态程序集/接口
    /// </summary>
    void RemoveAssembly(string assemblyName);
}