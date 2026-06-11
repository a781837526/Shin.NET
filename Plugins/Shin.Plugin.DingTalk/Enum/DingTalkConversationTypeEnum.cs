// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

/// <summary>
/// 钉钉发送的会话类型枚举
/// </summary>
[Description("钉钉发送的会话类型枚举")]
public enum DingTalkConversationTypeEnum
{
    /// <summary>
    /// 单聊
    /// </summary>
    [Description("单聊")]
    SingleChat = 0,

    /// <summary>
    /// 群聊
    /// </summary>
    [Description("群聊")]
    GroupChat = 1
}