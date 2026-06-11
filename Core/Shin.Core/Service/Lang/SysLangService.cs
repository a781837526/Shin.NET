// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 语言服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 100, Description = "语言服务")]
public partial class SysLangService : ISysLangService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysLang> _repository;

    /// <summary>
    /// 初始化<see cref="SysLangService"/>类的新实例
    /// </summary>
    public SysLangService(ISqlSugarRepository<SysLang> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 分页查询语言
    /// </summary>
    [DisplayName("分页查询语言")]
    [ApiDescriptionSettings(Name = "Page"), HttpPost]
    public async Task<SqlSugarPagedList<SysLangOutput>> Page(PageSysLangInput input)
    {
        input.Keyword = input.Keyword?.Trim();
        var query = _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Keyword), u => u.Name.Contains(input.Keyword) || u.Code.Contains(input.Keyword) || u.IsoCode.Contains(input.Keyword) || u.UrlCode.Contains(input.Keyword))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.IsoCode), u => u.IsoCode.Contains(input.IsoCode.Trim()))
            .WhereIF(!string.IsNullOrWhiteSpace(input.UrlCode), u => u.UrlCode.Contains(input.UrlCode.Trim()))
            .Select<SysLangOutput>();
        return await query.OrderBuilder(input).ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取语言详情 ℹ️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取语言详情")]
    [ApiDescriptionSettings(Name = "Detail"), HttpGet]
    public async Task<SysLang> Detail([FromQuery] QueryByIdSysLangInput input)
    {
        return await _repository.GetFirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 增加语言 ➕
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("增加语言")]
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    public async Task<long> Add(AddSysLangInput input)
    {
        var entity = input.Adapt<SysLang>();
        return await _repository.InsertAsync(entity) ? entity.Id : 0;
    }

    /// <summary>
    /// 更新语言 ✏️
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("更新语言")]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    public async Task Update(UpdateSysLangInput input)
    {
        var entity = input.Adapt<SysLang>();
        await _repository.AsUpdateable(entity)
        .ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除语言 ❌
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("删除语言")]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    public async Task Delete(DeleteSysLangInput input)
    {
        var entity = await _repository.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D1002);
        await _repository.DeleteAsync(entity);   //真删除
    }

    /// <summary>
    /// 获取下拉列表数据 🔖
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("获取下拉列表数据")]
    [ApiDescriptionSettings(Name = "DropdownData"), HttpPost]
    public async Task<dynamic> DropdownData()
    {
        var data = await _repository.Context.Queryable<SysLang>()
            .Where(m => m.Active == true)
            .Select(u => new
            {
                Code = u.Code,
                Value = u.UrlCode,
                Label = $"{u.Name}"
            }).ToListAsync();
        return data;
    }
}