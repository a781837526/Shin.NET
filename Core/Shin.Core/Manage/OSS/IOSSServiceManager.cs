// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using OnceMi.AspNetCore.OSS;

namespace Shin.Core;

/// <summary>
/// OSS服务管理器接口
/// </summary>
public interface IOSSServiceManager : ITransient, IDisposable
{
    /// <summary>
    /// 获取OSS服务实例
    /// </summary>
    /// <param name="provider">存储提供者配置</param>
    /// <returns></returns>
    Task<IOSSService> GetOSSServiceAsync(SysFileProvider provider);

    /// <summary>
    /// 清除缓存
    /// </summary>
    void ClearCache();
}