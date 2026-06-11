// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 参与方的标识类型枚举
/// </summary>
[SuppressSniffer]
[Description("参与方的标识类型枚举")]
public enum AlipayIdentityTypeEnum
{
    [Description("支付宝用户UID")]
    ALIPAY_USER_ID,

    [Description("支付宝登录号")]
    ALIPAY_LOGON_ID
}