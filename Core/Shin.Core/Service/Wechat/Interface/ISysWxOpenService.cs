// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 微信小程序服务接口
/// </summary>
public interface ISysWxOpenService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取微信用户OpenId
    /// </summary>
    Task<WxOpenIdOutput> GetWxOpenId(JsCode2SessionInput input);

    /// <summary>
    /// 获取微信用户电话号码
    /// </summary>
    Task<WxPhoneOutput> GetWxPhone(WxPhoneInput input);

    /// <summary>
    /// 微信小程序登录OpenId
    /// </summary>
    Task<dynamic> WxOpenIdLogin(WxOpenIdLoginInput input);

    /// <summary>
    /// 上传小程序头像
    /// </summary>
    Task<SysFile> UploadAvatar(UploadAvatarInput input);

    /// <summary>
    /// 设置小程序用户昵称
    /// </summary>
    Task SetNickName(SetNickNameInput input);

    /// <summary>
    /// 获取小程序用户信息
    /// </summary>
    Task<dynamic> GetUserInfo(string openid);

    /// <summary>
    /// 获取订阅消息模板列表
    /// </summary>
    Task<dynamic> GetMessageTemplateList();

    /// <summary>
    /// 发送订阅消息
    /// </summary>
    Task<dynamic> SendSubscribeMessage(SendSubscribeMessageInput input);

    /// <summary>
    /// 增加订阅消息模板
    /// </summary>
    Task<dynamic> AddSubscribeMessageTemplate(AddSubscribeMessageTemplateInput input);

    /// <summary>
    /// 生成带参数小程序二维码(总共生成的码数量限制为 100,000)
    /// </summary>
    /// <param name="input"> 扫码进入的小程序页面路径，最大长度 128 个字符，不能为空； eg: pages / index ? id = AY000001 </param>
    Task<GenerateQRImageOutput> GenerateQRImageAsync(GenerateQRImageInput input);

    /// <summary>
    /// 生成二维码(获取不受限制的小程序码)
    /// </summary>
    /// <param name="input">入参</param>
    Task<GenerateQRImageOutput> GenerateQRImageUnlimitAsync(GenerateQRImageUnLimitInput input);
}