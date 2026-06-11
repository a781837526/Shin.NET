// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public static class RequestExtension
{
    /// <summary>
    /// 获取请求地址源
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static string GetOrigin(this HttpRequest request)
    {
        string scheme = request.Scheme;
        string host = request.Host.Host;
        int port = request.Host.Port ?? (-1);

        string url = $"{scheme}://{host}";
        if (port != 80 && port != 443 && port != -1) url += $":{port}";

        return url;
    }
}