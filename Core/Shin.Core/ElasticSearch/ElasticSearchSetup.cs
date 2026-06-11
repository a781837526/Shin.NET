// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Shin.Core;

/// <summary>
/// ES服务注册
/// </summary>
public static class ElasticSearchSetup
{
    public static void AddElasticSearch(this IServiceCollection services)
    {
        var option = App.GetConfig<ElasticSearchOptions>("Logging:ElasticSearch");
        if (!option.Enabled) return;

        var uris = option.ServerUris.Select(u => new Uri(u));
        // 集群
        var connectionPool = new StaticNodePool(uris);
        var connectionSettings = new ElasticsearchClientSettings(connectionPool).DefaultIndex(option.DefaultIndex);
        // 单连接
        //var connectionSettings = new ElasticsearchClientSettings(new StaticNodePool(new List<Uri> { uris.FirstOrDefault() })).DefaultIndex(option.DefaultIndex);

        // 认证类型
        if (option.AuthType == ElasticSearchAuthTypeEnum.Basic) // Basic 认证
        {
            connectionSettings.Authentication(new BasicAuthentication(option.User, option.Password));
        }
        else if (option.AuthType == ElasticSearchAuthTypeEnum.ApiKey) // ApiKey 认证
        {
            connectionSettings.Authentication(new ApiKey(option.ApiKey));
        }
        else if (option.AuthType == ElasticSearchAuthTypeEnum.Base64ApiKey) // Base64ApiKey 认证
        {
            connectionSettings.Authentication(new Base64ApiKey(option.Base64ApiKey));
        }
        else return;

        // ES使用Https时的证书指纹
        if (!string.IsNullOrEmpty(option.Fingerprint))
        {
            connectionSettings.CertificateFingerprint(option.Fingerprint);
        }

        var client = new ElasticsearchClient(connectionSettings);
        services.AddSingleton(client); // 单例注册
    }
}