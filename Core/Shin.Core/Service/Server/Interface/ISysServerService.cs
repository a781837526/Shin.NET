// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

#if NET10_0_OR_GREATER

using XiHan.Framework.Utils.Core;
using XiHan.Framework.Utils.Reflections;
using XiHan.Framework.Utils.Runtime;

#endif // NET10_0_OR_GREATER

namespace Shin.Core.Interface;

/// <summary>
/// 系统服务器监控服务接口
/// </summary>
public interface ISysServerService : IDynamicApiController, ITransient
{
#if NET10_0_OR_GREATER
    /// <summary>
    /// 获取服务器硬件信息
    /// </summary>
    SystemInfo HardwareInfo();

    /// <summary>
    /// 获取服务器运行时信息
    /// </summary>
    RuntimeInfo RuntimeInfo();

    /// <summary>
    /// 获取框架主要程序集
    /// </summary>
    List<NuGetPackage> NuGetPackagesInfo();
#endif  // NET10_0_OR_GREATER

    /// <summary>
    /// 获取服务器配置信息
    /// </summary>
    dynamic GetServerBase();

    /// <summary>
    /// 获取服务器使用信息
    /// </summary>
    dynamic GetServerUsed();

    /// <summary>
    /// 获取服务器磁盘信息
    /// </summary>
    dynamic GetServerDisk();

    /// <summary>
    /// 获取框架主要程序集
    /// </summary>
    dynamic GetAssemblyList();
}