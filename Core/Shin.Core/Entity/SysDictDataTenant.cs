// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统租户字典值表
/// </summary>
[SugarTable(null, "系统租户字典值表")]
[SysTable]
[SugarIndex("index_{table}_TV", nameof(DictTypeId), OrderByType.Asc, nameof(Value), OrderByType.Asc)]
public partial class SysDictDataTenant : SysDictData, ITenantIdFilter
{
    /// <summary>
    /// 租户Id
    /// </summary>
    [SugarColumn(ColumnDescription = "租户Id", IsOnlyIgnoreUpdate = true)]
    public virtual long? TenantId { get; set; }
}