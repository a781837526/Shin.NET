// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 微信公众号服务接口
/// </summary>
public interface ISysWechatService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 生成网页授权Url
    /// </summary>
    string GenAuthUrl(GenAuthUrlInput input);

    /// <summary>
    /// 获取微信用户OpenId
    /// </summary>
    Task<string> SnsOAuth2(WechatOAuth2Input input);

    /// <summary>
    /// 微信用户登录OpenId
    /// </summary>
    Task<dynamic> OpenIdLogin(WechatUserLogin input);

    /// <summary>
    /// 获取配置签名参数(wx.config)
    /// </summary>
    Task<dynamic> GenConfigPara(SignatureInput input);

    /// <summary>
    /// 获取模板列表
    /// </summary>
    Task<dynamic> GetMessageTemplateList();

    /// <summary>
    /// 发送模板消息
    /// </summary>
    Task<dynamic> SendTemplateMessage(MessageTemplateSendInput input);

    /// <summary>
    /// 删除模板
    /// </summary>
    Task<dynamic> DeleteMessageTemplate(DeleteMessageTemplateInput input);

    /// <summary>
    /// 获取Access_token
    /// </summary>
    Task<string> GetCgibinToken();
}