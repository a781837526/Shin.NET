// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 设置状态输入参数
/// </summary>
public class BaseStatusInput : BaseIdInput
{
    /// <summary>
    /// 状态
    /// </summary>
    [Enum]
    public StatusEnum Status { get; set; }
}