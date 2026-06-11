// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统角色菜单
/// </summary>
public class RoleMenuInput : BaseIdInput
{
    /// <summary>
    /// 同步角色Id集合
    /// </summary>
    public List<long> RoleIdList { get; set; }

    /// <summary>
    /// 菜单Id集合
    /// </summary>
    public List<long> MenuIdList { get; set; }
}