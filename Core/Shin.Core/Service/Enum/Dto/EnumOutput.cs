// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 枚举类型输出参数
/// </summary>
public class EnumTypeOutput
{
    /// <summary>
    /// 枚举类型描述
    /// </summary>
    public string TypeDescribe { get; set; }

    /// <summary>
    /// 枚举类型名称
    /// </summary>
    public string TypeName { get; set; }

    /// <summary>
    /// 枚举类型全名称
    /// </summary>
    public string TypeFullName { get; set; }

    /// <summary>
    /// 枚举类型备注
    /// </summary>
    public string TypeRemark { get; set; }

    /// <summary>
    /// 枚举实体
    /// </summary>
    public List<EnumEntity> EnumEntities { get; set; }
}