// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Plugin.ApprovalFlow;

/// <summary>
/// 审批流程服务接口
/// </summary>
public interface IApprovalFlowService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 分页查询审批流
    /// </summary>
    /// <param name="input">审批流分页查询输入参数</param>
    Task<SqlSugarPagedList<ApprovalFlowOutput>> Page(ApprovalFlowInput input);

    /// <summary>
    /// 增加审批流
    /// </summary>
    /// <param name="input">审批流增加输入参数</param>
    Task<long> Add(AddApprovalFlowInput input);

    /// <summary>
    /// 更新审批流
    /// </summary>
    /// <param name="input">审批流更新输入参数</param>
    Task Update(UpdateApprovalFlowInput input);

    /// <summary>
    /// 删除审批流
    /// </summary>
    /// <param name="input">审批流删除输入参数</param>
    Task Delete(DeleteApprovalFlowInput input);

    /// <summary>
    /// 获取审批流
    /// </summary>
    /// <param name="input">审批流主键查询输入参数</param>
    Task<ApprovalFlow> GetDetail(QueryByIdApprovalFlowInput input);

    /// <summary>
    /// 根据编码获取审批流信息
    /// </summary>
    /// <param name="code">审批流程编号</param>
    Task<ApprovalFlow> GetInfo(string code);

    /// <summary>
    /// 获取审批流列表
    /// </summary>
    /// <param name="input">审批流分页查询输入参数</param>
    Task<List<ApprovalFlowOutput>> GetList(ApprovalFlowInput input);

    /// <summary>
    /// 获取审批流结构
    /// </summary>
    /// <param name="code">审批流程编号</param>
    Task<dynamic> FlowList(string code);

    /// <summary>
    /// 获取审批流规则
    /// </summary>
    Task<List<string>> FormRoutes();
}