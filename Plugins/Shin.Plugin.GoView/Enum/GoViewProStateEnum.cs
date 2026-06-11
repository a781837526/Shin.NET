// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.GoView;

/// <summary>
/// GoView 项目状态
/// </summary>
[Description("GoView 项目状态")]
public enum GoViewProStateEnum
{
    /// <summary>
    /// 未发布
    /// </summary>
    [Description("未发布")]
    UnPublish = -1,

    /// <summary>
    /// 已发布
    /// </summary>
    [Description("已发布")]
    Published = 1,
}