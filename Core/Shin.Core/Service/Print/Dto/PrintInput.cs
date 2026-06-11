// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class PagePrintInput : BasePageInput
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

public class AddPrintInput : SysPrint
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "模板名称不能为空")]
    public override string Name { get; set; }
}

public class UpdatePrintInput : AddPrintInput
{
}

public class DeletePrintInput : BaseIdInput
{
}