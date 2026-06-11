// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 批量启用/禁用存储提供者输入参数
/// </summary>
public class BatchEnableProviderInput
{
    /// <summary>
    /// 存储提供者ID列表
    /// </summary>
    [Required]
    public List<long> Ids { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnable { get; set; }
}