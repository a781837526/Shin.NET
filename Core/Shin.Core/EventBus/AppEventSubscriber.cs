// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 事件订阅
/// </summary>
public class AppEventSubscriber : IEventSubscriber, ISingleton, IDisposable
{
    private static readonly ISugarQueryable<SysTenant> SysTenantQueryable = App.GetService<ISqlSugarClient>().Queryable<SysTenant>();
    private readonly IServiceScope _serviceScope;

    public AppEventSubscriber(IServiceScopeFactory scopeFactory)
    {
        _serviceScope = scopeFactory.CreateScope();
    }

    /// <summary>
    /// 增加异常日志
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [EventSubscribe(CommonConst.AddExLog)]
    public async Task CreateExLog(EventHandlerExecutingContext context)
    {
        var db = _serviceScope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
        await db.CopyNew().Insertable((SysLogEx)context.Source.Payload).ExecuteCommandAsync();
    }

    /// <summary>
    /// 发送异常邮件
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [EventSubscribe(CommonConst.SendErrorMail)]
    public async Task SendOrderErrorMail(EventHandlerExecutingContext context)
    {
        long.TryParse(App.HttpContext?.User.FindFirst(ClaimConst.TenantId)?.Value, out var tenantId);
        var tenant = await SysTenantQueryable.FirstAsync(t => t.Id == tenantId);
        var title = $"{tenant?.Title} 系统异常";
        await _serviceScope.ServiceProvider.GetRequiredService<SysEmailService>().SendEmail(JSON.Serialize(context.Source.Payload), title);
    }

    /// <summary>
    /// 释放服务作用域
    /// </summary>
    public void Dispose()
    {
        _serviceScope.Dispose();
    }
}