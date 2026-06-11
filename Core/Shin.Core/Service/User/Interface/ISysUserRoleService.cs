// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统用户角色服务接口
/// </summary>
public interface ISysUserRoleService : ITransient
{
    /// <summary>
    /// 授权用户角色
    /// </summary>
    Task GrantUserRole(UserRoleInput input);

    /// <summary>
    /// 根据角色Id删除用户角色
    /// </summary>
    Task DeleteUserRoleByRoleId(long roleId);

    /// <summary>
    /// 根据用户Id删除用户角色
    /// </summary>
    Task DeleteUserRoleByUserId(long userId);

    /// <summary>
    /// 根据用户Id获取角色集合
    /// </summary>
    Task<List<SysRole>> GetUserRoleList(long userId);

    /// <summary>
    /// 根据用户Id获取角色Id集合
    /// </summary>
    Task<List<long>> GetUserRoleIdList(long userId);

    /// <summary>
    /// 根据角色Id获取用户Id集合
    /// </summary>
    Task<List<long>> GetUserIdList(long roleId);
}