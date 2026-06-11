// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统机构服务接口
/// </summary>
public interface ISysOrgService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取机构列表
    /// </summary>
    Task<List<SysOrg>> GetList(OrgInput input);

    /// <summary>
    /// 获取机构树
    /// </summary>
    Task<List<OrgTreeOutput>> GetTree([FromQuery] OrgInput input);

    /// <summary>
    /// 增加机构
    /// </summary>
    Task<long> AddOrg(AddOrgInput input);

    /// <summary>
    /// 批量增加机构
    /// </summary>
    Task BatchAddOrgs(List<SysOrg> orgs);

    /// <summary>
    /// 更新机构
    /// </summary>
    Task UpdateOrg(UpdateOrgInput input);

    /// <summary>
    /// 删除机构
    /// </summary>
    Task DeleteOrg(DeleteOrgInput input);

    /// <summary>
    /// 获取当前用户机构Id集合
    /// </summary>
    Task<List<long>> GetUserOrgIdList();

    /// <summary>
    /// 根据指定用户Id获取机构Id集合
    /// </summary>
    Task<List<long>> GetUserOrgIdList(long userId, long userOrgId);

    /// <summary>
    /// 判定用户是否有某角色权限
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="role">角色代码</param>
    Task<bool> GetUserHasRole(long userId, SysRole role);

    /// <summary>
    /// 根据节点Id获取子节点Id集合(包含自己)
    /// </summary>
    Task<List<long>> GetChildIdListWithSelfById(long pid);
}