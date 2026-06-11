// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.GoView;

/// <summary>
/// GoView 项目数据表
/// </summary>
[SugarTable(null, "GoView 项目数据表")]
[SysTable]
public class GoViewProData : EntityBaseTenant
{
    /// <summary>
    /// 项目内容
    /// </summary>
    [SugarColumn(ColumnDescription = "项目内容", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? Content { get; set; }

    /// <summary>
    /// 预览图片
    /// </summary>
    [SugarColumn(ColumnDescription = "预览图片", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? IndexImageData { get; set; }

    /// <summary>
    /// 背景图片
    /// </summary>
    [SugarColumn(ColumnDescription = "背景图片", ColumnDataType = StaticConfig.CodeFirst_BigString)]
    public string? BackGroundImageData { get; set; }
}