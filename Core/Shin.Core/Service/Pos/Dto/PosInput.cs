// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class PosInput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }
}

public class AddPosInput : SysPos
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "职位名称不能为空")]
    public override string Name { get; set; }
}

public class UpdatePosInput : AddPosInput
{
}

public class DeletePosInput : BaseIdInput
{
}