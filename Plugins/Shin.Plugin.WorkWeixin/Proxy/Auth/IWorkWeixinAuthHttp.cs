// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.WorkWeixin;

/// <summary>
/// 授权会话远程服务
/// </summary>
public interface IWorkWeixinAuthHttp : IHttpDeclarative
{
    /// <summary>
    /// 获取接口凭证
    /// </summary>
    /// <param name="corpId">企业ID</param>
    /// <param name="corpSecret">应用的凭证密钥</param>
    /// <returns></returns>
    /// <see href="https://developer.work.weixin.qq.com/document/path/91039"/>
    [Post("https://qyapi.weixin.qq.com/cgi-bin/gettoken")]
    Task<AuthAccessTokenHttpOutput> GetToken([QueryParam("corpid")] string corpId, [QueryParam("corpsecret")] string corpSecret);
}