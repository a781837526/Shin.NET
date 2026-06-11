// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.K3Cloud;

/// <summary>
/// ERP基础入参
/// </summary>
public class K3CloudBaeInput<T>
{
    /// <summary>
    /// 表单Id
    /// </summary>
    public string formid { get; set; }

    /// <summary>
    /// 数据包
    /// </summary>
    public T data { get; set; }
}