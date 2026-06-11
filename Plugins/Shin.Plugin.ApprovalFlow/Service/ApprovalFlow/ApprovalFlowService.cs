// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using System.Text.Json;

namespace Shin.Plugin.ApprovalFlow;

/// <summary>
/// 审批流程服务 🧩
/// </summary>
[ApiDescriptionSettings(ApprovalFlowConst.GroupName, Order = 100)]
public class ApprovalFlowService : IApprovalFlowService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<ApprovalFlow> _repository;

    /// <summary>
    /// 初始化<see cref="ApprovalFlowService"/>类的新实例
    /// </summary>
    public ApprovalFlowService(ISqlSugarRepository<ApprovalFlow> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 分页查询审批流 🔖
    /// </summary>
    /// <param name="input">审批流分页查询输入参数</param>
    [HttpPost("Page")]
    public async Task<SqlSugarPagedList<ApprovalFlowOutput>> Page(ApprovalFlowInput input)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Code.Contains(input.Keyword.Trim()) || u.Name.Contains(input.Keyword.Trim()) || u.Remark.Contains(input.Keyword.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Remark), u => u.Remark.Contains(input.Remark.Trim()))
            .Select<ApprovalFlowOutput>()
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加审批流 🔖
    /// </summary>
    /// <param name="input">审批流增加输入参数</param>
    [HttpPost("Add")]
    public async Task<long> Add(AddApprovalFlowInput input)
    {
        var entity = input.Adapt<ApprovalFlow>();
        if (input.Code == null)
        {
            entity.Code = await LastCode("");
        }
        await _repository.InsertAsync(entity);
        return entity.Id;
    }

    /// <summary>
    /// 更新审批流 🔖
    /// </summary>
    /// <param name="input">审批流更新输入参数</param>
    [HttpPost("Update")]
    public async Task Update(UpdateApprovalFlowInput input)
    {
        var entity = input.Adapt<ApprovalFlow>();
        await _repository.AsUpdateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除审批流 🔖
    /// </summary>
    /// <param name="input">审批流删除输入参数</param>
    [HttpPost("Delete")]
    public async Task Delete(DeleteApprovalFlowInput input)
    {
        var entity = await _repository.GetByIdAsync(input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _repository.FakeDeleteAsync(entity);  // 假删除
    }

    /// <summary>
    /// 获取审批流 🔖
    /// </summary>
    /// <param name="input">审批流主键查询输入参数</param>
    public async Task<ApprovalFlow> GetDetail([FromQuery] QueryByIdApprovalFlowInput input)
    {
        return await _repository.GetByIdAsync(input.Id);
    }

    /// <summary>
    /// 根据编码获取审批流信息 🔖
    /// </summary>
    /// <param name="code">审批流程编号</param>
    [HttpGet]
    public async Task<ApprovalFlow> GetInfo([FromQuery] string code)
    {
        return await _repository.GetFirstAsync(u => u.Code == code);
    }

    /// <summary>
    /// 获取审批流列表 🔖
    /// </summary>
    /// <param name="input">审批流分页查询输入参数</param>
    [HttpGet]
    public async Task<List<ApprovalFlowOutput>> GetList([FromQuery] ApprovalFlowInput input)
    {
        return await _repository.AsQueryable().Select<ApprovalFlowOutput>().ToListAsync();
    }

    /// <summary>
    /// 获取审批流结构 🔖
    /// </summary>
    /// <param name="code">审批流程编号</param>
    [HttpGet("FlowList")]
    public async Task<dynamic> FlowList([FromQuery] string code)
    {
        var result = await _repository.AsQueryable().Where(u => u.Code == code).Select<ApprovalFlowOutput>().FirstAsync();
        var FlowJson = result.FlowJson != null ? JsonSerializer.Deserialize<ApprovalFlowItem>(result.FlowJson) : new ApprovalFlowItem();
        var FormJson = result.FormJson != null ? JsonSerializer.Deserialize<ApprovalFormItem>(result.FormJson) : new ApprovalFormItem();
        return new
        {
            FlowJson,
            FormJson
        };
    }

    /// <summary>
    /// 获取审批流规则 🔖
    /// </summary>
    [HttpGet("FormRoutes")]
    public async Task<List<string>> FormRoutes()
    {
        var results = await _repository.AsQueryable().Select<ApprovalFlowOutput>().ToListAsync();
        var list = new List<string>();
        foreach (var item in results)
        {
            var FormJson = item.FormJson != null ? JsonSerializer.Deserialize<ApprovalFormItem>(item.FormJson) : new ApprovalFormItem();
            if (item.FormJson != null) list.Add(FormJson.Route);
        }
        return list;
    }

    #region Private

    /// <summary>
    /// 获取今天创建的最大编号
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    private async Task<string> LastCode(string prefix)
    {
        var today = DateTime.Now.Date;
        var count = await _repository.AsQueryable().Where(u => u.CreateTime >= today).CountAsync();
        return prefix + DateTime.Now.ToString("yyMMdd") + string.Format("{0:d2}", count + 1);
    }

    #endregion Private
}