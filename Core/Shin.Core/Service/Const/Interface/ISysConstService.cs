// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统常量服务接口
/// </summary>
public interface ISysConstService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取所有常量列表
    /// </summary>
    Task<List<ConstOutput>> GetList();

    /// <summary>
    /// 根据类名获取常量数据
    /// </summary>
    Task<List<ConstOutput>> GetData(string typeName);
}