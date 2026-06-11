// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 当前登录用户接口
/// </summary>
public interface IUserManager : IScoped
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public long TenantId { get; }

    /// <summary>
    /// 用户账号
    /// </summary>
    public string Account { get; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; }

    /// <summary>
    /// 账号类型
    /// </summary>
    public AccountTypeEnum? AccountType { get; }

    /// <summary>
    /// 是否超级管理员
    /// </summary>
    public bool SuperAdmin { get; }

    /// <summary>
    /// 是否系统管理员
    /// </summary>
    public bool SysAdmin { get; }

    /// <summary>
    /// 组织机构Id
    /// </summary>
    public long OrgId { get; }

    /// <summary>
    /// 微信OpenId
    /// </summary>
    public string OpenId { get; }

    /// <summary>
    /// 语言代码
    /// </summary>
    public string LangCode { get; }
}