// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 作业执行监视器
/// </summary>
public class JobMonitor : IJobMonitor
{
    /// <summary>
    /// 服务作用域工厂
    /// </summary>
    private readonly IServiceScopeFactory _serviceScopeFactory;

    /// <summary>
    /// 事件发布服务依赖
    /// </summary>
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// 作业执行监视器日志
    /// </summary>
    private readonly ILogger<JobMonitor> _logger;

    /// <summary>
    /// 初始化<see cref="JobMonitor"/>类的新实例
    /// </summary>
    public JobMonitor(IServiceScopeFactory serviceScopeFactory, IEventPublisher eventPublisher, ILogger<JobMonitor> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public Task OnExecutingAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }

    public async Task OnExecutedAsync(JobExecutedContext context, CancellationToken stoppingToken)
    {
        if (context.Exception == null) return;

        var exception = $"定时任务【{context.Trigger.Description}】错误：{context.Exception}";
        // 将作业异常信息记录到本地
        _logger.LogError(exception);

        using var scope = _serviceScopeFactory.CreateScope();
        var sysConfigService = scope.ServiceProvider.GetRequiredService<SysConfigService>();
        if (await sysConfigService.GetConfigValue<bool>(ConfigConst.SysErrorMail))
        {
            // 将作业异常信息发送到邮件
            await _eventPublisher.PublishAsync(CommonConst.SendErrorMail, exception, stoppingToken);
        }
    }
}