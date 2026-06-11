// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection.Extensions;
using NewLife.Caching.Services;

namespace Shin.Core;

/// <summary>
/// 系统缓存注册
/// </summary>
public static class CacheSetup
{
    /// <summary>
    /// 缓存注册（新生命Redis组件）
    /// </summary>
    /// <param name="services"></param>
    public static void AddCache(this IServiceCollection services)
    {
        var cacheOptions = App.GetConfig<CacheOptions>("Cache", true);
        if (cacheOptions.CacheType == CacheTypeEnum.Redis.ToString())
        {
            var redis = new FullRedis(new RedisOptions
            {
                Configuration = cacheOptions.Redis.Configuration,
                Prefix = cacheOptions.Redis.Prefix
            })
            {
                // 自动检测集群节点
                AutoDetect = App.GetConfig<bool>("Cache:Redis:AutoDetect", true)
            };
            // 最大消息大小
            if (cacheOptions.Redis.MaxMessageSize > 0)
                redis.MaxMessageSize = cacheOptions.Redis.MaxMessageSize;

            // 注入 Redis 缓存提供者
            services.AddSingleton<ICacheProvider>(u => new RedisCacheProvider(u) { Cache = redis });
        }

        // 内存缓存兜底。在没有配置Redis时，使用内存缓存，逻辑代码无需修改
        services.TryAddSingleton<ICacheProvider, CacheProvider>();
    }
}