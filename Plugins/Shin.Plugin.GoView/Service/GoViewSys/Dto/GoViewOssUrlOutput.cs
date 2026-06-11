// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.GoView;

/// <summary>
/// 获取 OSS 上传接口输出
/// </summary>
public class GoViewOssUrlOutput
{
    /// <summary>
    /// 桶名
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// BucketURL 地址
    /// </summary>
    public string BucketURL { get; set; }
}