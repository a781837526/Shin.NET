// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 国密公钥私钥对输出
/// </summary>
public class SmKeyPairOutput
{
    /// <summary>
    /// 私匙
    /// </summary>
    public string PrivateKey { get; set; }

    /// <summary>
    /// 公匙
    /// </summary>
    public string PublicKey { get; set; }
}