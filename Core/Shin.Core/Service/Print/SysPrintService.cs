// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统打印模板服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 305)]
public class SysPrintService : ISysPrintService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysPrint> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 初始化<see cref="SysPrintService"/>类的新实例
    /// </summary>
    public SysPrintService(ISqlSugarRepository<SysPrint> repository,
        IUserManager userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    /// <summary>
    /// 获取打印模板列表 🖨️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取打印模板列表")]
    public async Task<SqlSugarPagedList<SysPrint>> Page(PagePrintInput input)
    {
        return await _repository.AsQueryable()
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name))
            .OrderBy(u => new { u.OrderNo, u.Id })
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取打印模板 🖨️
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [DisplayName("获取打印模板")]
    public async Task<SysPrint> GetPrint(string name)
    {
        return await _repository.GetFirstAsync(u => u.Name == name);
    }

    /// <summary>
    /// 增加打印模板 🖨️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加打印模板")]
    public async Task AddPrint(AddPrintInput input)
    {
        var isExist = await _repository.IsAnyAsync(u => u.Name == input.Name);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1800);

        await _repository.InsertAsync(input.Adapt<SysPrint>());
    }

    /// <summary>
    /// 更新打印模板 🖨️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新打印模板")]
    public async Task UpdatePrint(UpdatePrintInput input)
    {
        var isExist = await _repository.IsAnyAsync(u => u.Name == input.Name && u.Id != input.Id);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D1800);

        await _repository.AsUpdateable(input.Adapt<SysPrint>()).IgnoreColumns(true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除打印模板 🖨️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除打印模板")]
    public async Task DeletePrint(DeletePrintInput input)
    {
        await _repository.DeleteAsync(u => u.Id == input.Id);
    }
}