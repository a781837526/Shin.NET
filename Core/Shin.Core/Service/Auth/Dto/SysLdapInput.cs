// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统域登录信息配置输入参数
/// </summary>
public class SysLdapInput : BasePageInput
{
    /// <summary>
    /// 主机
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }
}

public class AddSysLdapInput : SysLdap
{
}

public class UpdateSysLdapInput : SysLdap
{
}

public class DeleteSysLdapInput : BaseIdInput
{
}

public class DetailSysLdapInput : BaseIdInput
{
}

public class SyncSysLdapInput : BaseIdInput
{
}