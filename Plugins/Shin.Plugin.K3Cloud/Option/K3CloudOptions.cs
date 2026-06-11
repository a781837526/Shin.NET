// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.K3Cloud;

public sealed class K3CloudOptions : IConfigurableOptions
{
    /// <summary>
    /// ERP业务站点地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 帐套Id(数据中心ID)
    /// </summary>
    public string AcctID { get; set; }

    /// <summary>
    /// 应用Id
    /// </summary>
    public string AppId { get; set; }

    /// <summary>
    /// 应用密钥
    /// </summary>
    public string AppKey { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户密码
    /// </summary>
    public string UserPassword { get; set; }

    /// <summary>
    /// 语言代码
    /// </summary>
    public string LanguageCode { get; set; }
}