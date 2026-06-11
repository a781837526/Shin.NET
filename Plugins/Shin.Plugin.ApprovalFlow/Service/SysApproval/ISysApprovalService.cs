// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.ApprovalFlow;

/// <summary>
/// 系统审批流程服务接口
/// </summary>
public interface ISysApprovalService : ITransient
{
    /// <summary>
    /// 匹配审批流程
    /// </summary>
    /// <param name="context">Http上下文</param>
    Task MatchApproval(HttpContext context);
}