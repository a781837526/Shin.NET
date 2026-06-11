// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 参与方的证件类型枚举
/// </summary>
[SuppressSniffer]
[Description("参与方的证件类型枚举")]
public enum AlipayCertTypeEnum
{
    [Description("身份证")]
    IDENTITY_CARD,

    [Description("护照")]
    PASSPORT
}