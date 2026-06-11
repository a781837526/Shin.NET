// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统租户配置参数值表
/// </summary>
[SugarTable(null, "系统租户配置参数值表")]
[SysTable]
[SugarIndex("index_{table}_UC", nameof(UserId), OrderByType.Asc, nameof(ConfigId), OrderByType.Asc)]
public class SysUserConfigData : EntityBaseId
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [SugarColumn(ColumnDescription = "用户Id")]
    public long UserId { get; set; }

    /// <summary>
    /// 配置项Id
    /// </summary>
    [SugarColumn(ColumnDescription = "配置项Id")]
    public long ConfigId { get; set; }

    /// <summary>
    /// 参数值
    /// </summary>
    [SugarColumn(ColumnDescription = "参数值", Length = 512)]
    [MaxLength(512)]
    public string? Value { get; set; }
}