// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class EventHandlerMonitor : IEventHandlerMonitor
{
    public Task OnExecutingAsync(EventHandlerExecutingContext context)
    {
        //_logger.LogInformation("执行之前：{EventId}", context.Source.EventId);
        return Task.CompletedTask;
    }

    public Task OnExecutedAsync(EventHandlerExecutedContext context)
    {
        //_logger.LogInformation("执行之后：{EventId}", context.Source.EventId);

        if (context.Exception != null)
        {
            Log.Error($"EventHandlerMonitor.OnExecutedAsync 执行出错啦：{context.Source.EventId}", context.Exception);
        }

        return Task.CompletedTask;
    }
}