// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统用户配置参数表
/// </summary>
[SugarTable(null, "系统用户配置参数表")]
[SysTable]
public partial class SysUserConfig : SysConfig
{
    /// <summary>
    /// 无效字段，用于忽略实体类的Value字段
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    private new string? Value { get; set; }
}