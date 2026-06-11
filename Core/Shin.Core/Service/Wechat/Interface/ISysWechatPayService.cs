// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 微信支付服务接口
/// </summary>
public interface ISysWechatPayService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 分页查询支付列表
    /// </summary>
    Task<SqlSugarPagedList<SysWechatPay>> Page(WechatPayPageInput input);

    /// <summary>
    /// 查询退款信息列表
    /// </summary>
    Task<List<SysWechatRefund>> ListRefund(string id);

    /// <summary>
    /// 生成JSAPI调起支付所需参数
    /// </summary>
    WechatPayParaOutput GenerateParametersForJsapiPay(WechatPayParaInput input);

    /// <summary>
    /// 微信支付下单(商户直连)
    /// </summary>
    Task<WechatPayTransactionOutput> CreatePayTransaction(WechatPayTransactionInput input);

    /// <summary>
    /// 微信支付下单(商户直连)Native
    /// </summary>
    Task<dynamic> CreatePayTransactionNative(WechatPayTransactionInput input);

    /// <summary>
    /// 微信支付下单(服务商模式)
    /// </summary>
    Task<dynamic> CreatePayPartnerTransaction(WechatPayTransactionInput input);

    /// <summary>
    /// 获取支付订单详情(本地库)
    /// </summary>
    Task<SysWechatPay> GetPayInfo(string tradeId);

    /// <summary>
    /// 获取支付订单详情(微信接口)
    /// </summary>
    Task<SysWechatPay> GetPayInfoFromWechat(string tradeId);

    /// <summary>
    /// 退款申请
    /// </summary>
    Task<dynamic> CreateRefundDomestic(WechatPayRefundDomesticInput input);

    /// <summary>
    /// 获取退款订单详情(微信接口)
    /// </summary>
    Task<SysWechatRefund> GetRefundInfoFromWechat(string refundId);

    /// <summary>
    /// 微信支付成功回调(商户直连)
    /// </summary>
    Task<WechatPayOutput> PayCallBack();

    /// <summary>
    /// 微信支付成功回调(服务商模式)
    /// </summary>
    Task PayPartnerCallBack();
}