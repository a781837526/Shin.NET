// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.ApprovalFlow;

/// <summary>
/// 系统审批流程服务 🧩
/// </summary>
public class SysApprovalService : ISysApprovalService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<ApprovalFlowRecord> _repository;

    /// <summary>
    /// 审批流程服务
    /// </summary>
    private readonly IApprovalFlowService _approvalFlowService;

    /// <summary>
    /// 初始化<see cref="SysApprovalService"/>类的新实例
    /// </summary>
    public SysApprovalService(ISqlSugarRepository<ApprovalFlowRecord> repository,
        IApprovalFlowService approvalFlowService)
    {
        _repository = repository;
        _approvalFlowService = approvalFlowService;
    }

    /// <summary>
    /// 匹配审批流程
    /// </summary>
    /// <param name="context">Http上下文</param>
    [NonAction]
    public async Task MatchApproval(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        var path = request.Path.ToString().Split("/");

        var method = request.Method;
        var qs = request.QueryString;
        var h = request.Headers;
        var b = request.Body;

        var requestHeaders = request.Headers;
        var responseHeaders = response.Headers;

        var serviceName = path[1];
        if (serviceName.StartsWith("api"))
        {
            if (path.Length > 3)
            {
                var funcName = path[2];
                var typeName = path[3];

                var list = await _approvalFlowService.FormRoutes();
                if (list.Any(u => u.Contains(funcName) && u.Contains(typeName)))
                {
                    var approvalFlow = new ApprovalFlowRecord
                    {
                        FormName = funcName,
                        CreateTime = DateTime.Now,
                    };

                    // 判断是否需要审批
                    await _repository.InsertAsync(approvalFlow);

                    var approvalForm = new ApprovalFormRecord
                    {
                        FlowId = approvalFlow.Id,
                        FormName = funcName,
                        FormType = typeName,
                        CreateTime = DateTime.Now,
                    };

                    // 判断是否需要审批
                    await _repository.Context.Insertable(approvalForm).ExecuteCommandAsync();
                }
            }
        }

        await Task.CompletedTask;
    }
}