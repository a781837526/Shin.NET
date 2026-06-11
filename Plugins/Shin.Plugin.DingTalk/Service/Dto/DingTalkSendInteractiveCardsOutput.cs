// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

/// <summary>
/// 发送钉钉互动卡片返回
/// </summary>
public class DingTalkSendInteractiveCardsOutput
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 创建卡片结果
    /// </summary>
    public DingTalkSendInteractiveCardsResult Result { get; set; }
}