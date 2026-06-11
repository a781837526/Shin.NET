// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using AspNetCoreRateLimit;
using Furion;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Shin.Core;

namespace Shin.Web.Core;

public static class ProjectOptions
{
    /// <summary>
    /// 注册项目配置选项
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddProjectOptions(this IServiceCollection services)
    {
        services.AddConfigurableOptions<DbConnectionOptions>();
        services.AddConfigurableOptions<SnowIdOptions>();
        services.AddConfigurableOptions<CacheOptions>();
        services.AddConfigurableOptions<ClusterOptions>();
        services.AddConfigurableOptions<OSSProviderOptions>();
        services.AddConfigurableOptions<UploadOptions>();
        services.AddConfigurableOptions<WechatOptions>();
        services.AddConfigurableOptions<WechatPayOptions>();
        services.AddConfigurableOptions<PayCallBackOptions>();
        services.AddConfigurableOptions<CodeGenOptions>();
        services.AddConfigurableOptions<EnumOptions>();
        services.AddConfigurableOptions<APIJSONOptions>();
        services.AddConfigurableOptions<EmailOptions>();
        services.AddConfigurableOptions<OAuthOptions>();
        services.AddConfigurableOptions<CryptogramOptions>();
        services.AddConfigurableOptions<SMSOptions>();
        services.AddConfigurableOptions<EventBusOptions>();
        services.AddConfigurableOptions<AlipayOptions>();
        services.AddConfigurableOptions<CDConfigOptions>();
        services.AddConfigurableOptions<DeepSeekOptions>();
        services.AddConfigurableOptions<LocalizationSettingsOptions>();
        services.Configure<IpRateLimitOptions>(App.Configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(App.Configuration.GetSection("IpRateLimitPolicies"));
        services.Configure<ClientRateLimitOptions>(App.Configuration.GetSection("ClientRateLimiting"));
        services.Configure<ClientRateLimitPolicies>(App.Configuration.GetSection("ClientRateLimitPolicies"));

        return services;
    }

    /// <summary>
    /// Scalar第三方SwaggerUI默认配置
    /// </summary>
    public static ScalarOptions SetDefaultScalarOptions(this ScalarOptions options)
    {
        options.Title = "Shin.NET API Reference";   //默认文档标题
        options.ShowSidebar = true;                 //是否显示侧边栏
        options.DefaultOpenAllTags = false;         //是否展开所有Tags标签
        options.ExpandAllModelSections = false;     //是否默认展开所有模型部分
        options.ExpandAllResponses = false;         //是否展开所有响应
        options.HideClientButton = false;           //是否隐藏API客户端入口
        options.HideDarkModeToggle = false;         //是否隐藏明暗风格切换按钮
        options.HideModels = false;                 //是否在侧边栏、搜索和内容中隐藏控制模型
        options.HideSearch = false;                 //是否隐藏搜索框
        options.HideTestRequestButton = false;      //是否隐藏测试接口按钮
        options.PersistentAuthentication = false;   //是否在本地存储中持久化认证状态
        options.Telemetry = true;                   //是否启用遥测
        options.Layout = ScalarLayout.Modern;       //布局样式

        return options;
    }
}