// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class DictTypeInput : BaseStatusInput
{
}

public class PageDictTypeInput : BasePageInput
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
}

public class AddDictTypeInput : SysDictType
{
    /// <summary>
    /// 是否是租户字典（Y-是，N-否）
    /// </summary>
    public override YesNoEnum IsTenant { get; set; } = YesNoEnum.Y;

    /// <summary>
    /// 是否是内置字典（Y-是，N-否）
    /// </summary>
    public override YesNoEnum SysFlag { get; set; } = YesNoEnum.N;
}

public class UpdateDictTypeInput : AddDictTypeInput
{
}

public class DeleteDictTypeInput : BaseIdInput
{
}

public class GetDataDictTypeInput
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "字典类型编码不能为空")]
    public string Code { get; set; }
}