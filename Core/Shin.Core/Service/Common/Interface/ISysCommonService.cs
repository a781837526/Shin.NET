// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统通用服务接口
/// </summary>
public interface ISysCommonService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取国密公钥私钥对
    /// </summary>
    SmKeyPairOutput GetSmKeyPair();

    /// <summary>
    /// 获取所有接口/动态API
    /// </summary>
    List<ApiOutput> GetApiList();

    /// <summary>
    /// 下载标记错误的临时Excel（全局）
    /// </summary>
    Task<IActionResult> DownloadErrorExcelTemp(string fileName = null);

    /// <summary>
    /// 加密字符串
    /// </summary>
    dynamic EncryptPlainText(string plainText);

    /// <summary>
    /// 接口压测
    /// </summary>
    Task<StressTestOutput> StressTest(StressTestInput input);
}