// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Aop.Api.Response;

namespace Shin.Core.Interface;

/// <summary>
/// 支付宝支付服务接口
/// </summary>
public interface ISysAlipayService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取授权信息
    /// </summary>
    ActionResult GetAuthInfo(AlipayAuthInfoInput input);

    /// <summary>
    /// 支付回调
    /// </summary>
    string Notify();

    /// <summary>
    ///  统一收单下单并支付页面接口
    /// </summary>
    string AlipayTradePagePay(AlipayTradePagePayInput input);

    /// <summary>
    ///  交易预创建
    /// </summary>
    string AlipayPreCreate(AlipayPreCreateInput input);

    /// <summary>
    /// 单笔转账到支付宝账户
    ///  https://opendocs.alipay.com/open/62987723_alipay.fund.trans.uni.transfer
    /// </summary>
    Task<AlipayFundTransUniTransferResponse> Transfer(AlipayFundTransUniTransferInput input);
}