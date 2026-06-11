// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统短信服务接口
/// </summary>
public interface ISysSmsService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="templateId">短信模板id</param>
    Task SendSms([Required] string phoneNumber, string templateId = "0");

    /// <summary>
    /// 校验短信验证码
    /// </summary>
    bool VerifyCode(SmsVerifyCodeInput input);

    /// <summary>
    /// 阿里云发送短信
    /// </summary>
    /// <param name="phoneNumber">手机号</param>
    /// <param name="templateId">短信模板id</param>
    Task AliyunSendSms(string phoneNumber, string templateId = "0");

    /// <summary>
    /// 发送短信模板
    /// </summary>
    /// <param name="phoneNumber">手机号</param>
    /// <param name="templateParam">短信内容</param>
    /// <param name="templateId">短信模板id</param>
    Task AliyunSendSmsTemplate(string phoneNumber, dynamic templateParam, string templateId);

    /// <summary>
    /// 腾讯云发送短信
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="templateId">短信模板id</param>
    Task TencentSendSms([Required] string phoneNumber, string templateId = "0");

    /// <summary>
    /// 自定义短信接口发送短信
    /// </summary>
    /// <param name="phoneNumber">手机号</param>
    /// <param name="templateId">短信模板id</param>
    Task CustomSendSms(string phoneNumber, string templateId = "0");
}