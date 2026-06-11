// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统用户服务接口
/// </summary>
public interface ISysUserService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取用户分页列表
    /// </summary>
    Task<SqlSugarPagedList<UserOutput>> Page(PageUserInput input);

    /// <summary>
    /// 增加用户
    /// </summary>
    Task<long> AddUser(AddUserInput input);

    /// <summary>
    /// 注册用户
    /// </summary>
    Task<long> RegisterUser(AddUserInput input);

    /// <summary>
    /// 更新用户
    /// </summary>
    Task UpdateUser(UpdateUserInput input);

    /// <summary>
    /// 更新当前用户语言
    /// </summary>
    Task SetLangCode(string langCode);

    /// <summary>
    /// 删除用户
    /// </summary>
    Task DeleteUser(DeleteUserInput input);

    /// <summary>
    /// 查看用户基本信息
    /// </summary>
    Task<SysUser> GetBaseInfo();

    /// <summary>
    /// 查询用户组织机构信息
    /// </summary>
    Task<List<OrgTreeOutput>> GetOrgInfo();

    /// <summary>
    /// 更新用户基本信息
    /// </summary>
    Task<int> UpdateBaseInfo(SysUser user);

    /// <summary>
    /// 设置用户状态
    /// </summary>
    Task<int> SetStatus(UserInput input);

    /// <summary>
    /// 授权用户角色
    /// </summary>
    Task GrantRole(UserRoleInput input);

    /// <summary>
    /// 修改用户密码
    /// </summary>
    Task<int> ChangePwd(ChangePwdInput input);

    /// <summary>
    /// 重置用户密码
    /// </summary>
    Task<string> ResetPwd(ResetPwdUserInput input);

    /// <summary>
    /// 解除登录锁定
    /// </summary>
    Task UnlockLogin(UnlockLoginInput input);

    /// <summary>
    /// 获取用户拥有角色集合
    /// </summary>
    Task<List<long>> GetOwnRoleList(long userId);

    /// <summary>
    /// 获取用户扩展机构集合
    /// </summary>
    Task<List<SysUserExtOrg>> GetOwnExtOrgList(long userId);
}