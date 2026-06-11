// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class JobDetailInput
{
    /// <summary>
    /// 作业Id
    /// </summary>
    public string JobId { get; set; }
}

public class PageJobDetailInput : BasePageInput
{
    /// <summary>
    /// 作业Id
    /// </summary>
    public string JobId { get; set; }

    /// <summary>
    /// 组名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 描述信息
    /// </summary>
    public string Description { get; set; }
}

public class AddJobDetailInput : SysJobDetail
{
    /// <summary>
    /// 作业Id
    /// </summary>
    [Required(ErrorMessage = "作业Id不能为空"), MinLength(2, ErrorMessage = "作业Id不能少于2个字符")]
    public override string JobId { get; set; }
}

public class UpdateJobDetailInput : AddJobDetailInput
{
}

public class DeleteJobDetailInput : JobDetailInput
{
}