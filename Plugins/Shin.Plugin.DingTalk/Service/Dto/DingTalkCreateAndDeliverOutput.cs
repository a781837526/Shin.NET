// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.DingTalk;

public class DingTalkCreateAndDeliverOutput
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public bool success { get; set; }

    /// <summary>
    /// 创建卡片结果
    /// </summary>
    public DingTalkCreateAndDeliverResult result { get; set; }

    public string code { get; set; }
    public string requestid { get; set; }
    public string message { get; set; }
}

public class DingTalkCreateAndDeliverResult
{
    /// <summary>
    /// 用于业务方后续查看已读列表的查询key
    /// </summary>
    public string processQueryKey { get; set; }
}