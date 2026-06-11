// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class WechatPayOutput
{
    /// <summary>
    /// OpenId
    /// </summary>
    public string OpenId { get; set; }

    /// <summary>
    /// 订单金额
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public string Attachment { get; set; }

    /// <summary>
    /// 优惠标记
    /// </summary>
    public string GoodsTag { get; set; }
}

public class WechatPayTransactionOutput
{
    public string PrepayId { get; set; }

    public string OutTradeNumber { get; set; }

    public WechatPayParaOutput SingInfo { get; set; }
}

public class WechatPayParaOutput
{
    public string AppId { get; set; }

    public string TimeStamp { get; set; }

    public string NonceStr { get; set; }

    public string Package { get; set; }

    public string SignType { get; set; }

    public string PaySign { get; set; }
}