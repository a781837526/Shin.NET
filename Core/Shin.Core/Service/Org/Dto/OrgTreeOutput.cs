// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 机构树形输出
/// </summary>
public class OrgTreeOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(IsTreeKey = true)]
    public long Id { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }

    /// <summary>
    /// 父Id
    /// </summary>
    public long Pid { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 机构子项
    /// </summary>
    public List<OrgTreeOutput> Children { get; set; }

    /// <summary>
    /// 是否禁止选中
    /// </summary>
    public bool Disabled { get; set; }
}