// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统字典类型服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 430, Description = "系统字典类型")]
public class SysDictTypeService : ISysDictTypeService
{
    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysDictType> _repository;

    /// <summary>
    /// 系统字典表查询器
    /// </summary>
    private readonly ISugarQueryable<SysDictData> _sysDictDataQuery;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 系统字典值服务
    /// </summary>
    private readonly ISysDictDataService _sysDictDataService;

    /// <summary>
    /// 翻译缓存服务
    /// </summary>
    private readonly ISysLangTextCacheService _sysLangTextCacheService;

    public SysDictTypeService(ISqlSugarRepository<SysDictType> repository,
        ISysDictDataService sysDictDataService,
        ICacheManager cacheManager,
        IUserManager userManager,
        ISysLangTextCacheService sysLangTextCacheService)
    {
        _repository = repository;
        _sysDictDataService = sysDictDataService;
        _cacheManager = cacheManager;
        _userManager = userManager;
        _sysLangTextCacheService = sysLangTextCacheService;

        _sysDictDataQuery = _repository.Context.UnionAll(
            _repository.Context.Queryable<SysDictData>(),
            _repository.Change<SysDictDataTenant>().AsQueryable()
            .Select<SysDictData>());
    }

    /// <summary>
    /// 获取字典类型分页列表 🔖
    /// </summary>
    [DisplayName("获取字典类型分页列表")]
    public async Task<SqlSugarPagedList<SysDictType>> Page(PageDictTypeInput input)
    {
        var langCode = _userManager.LangCode;
        var baseQuery = _repository.AsQueryable()
            .WhereIF(!_userManager.SuperAdmin, u => u.IsTenant == YesNoEnum.Y)
            .WhereIF(!string.IsNullOrEmpty(input.Code?.Trim()), u => u.Code.Contains(input.Code))
            .WhereIF(!string.IsNullOrEmpty(input.Name?.Trim()), u => u.Name.Contains(input.Name));
        //.OrderBy(u => new { u.OrderNo, u.Code })
        var pageList = await baseQuery.ToPagedListAsync(input.Page, input.PageSize);
        var list = pageList.Items;
        var ids = list.Select(d => d.Id).Distinct().ToList();
        var translations = await _sysLangTextCacheService.GetTranslations(
                               "SysDictType",
                               "Name",
                               ids,
                               langCode);
        foreach (var item in list)
        {
            if (translations.TryGetValue(item.Id, out var translatedName) && !string.IsNullOrEmpty(translatedName))
            {
                item.Name = translatedName;
            }
        }
        pageList.Items = list;
        return pageList;
    }

    /// <summary>
    /// 获取字典类型列表 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取字典类型列表")]
    public async Task<List<SysDictType>> GetList()
    {
        var langCode = _userManager.LangCode;
        var list = await _repository.AsQueryable().OrderBy(u => new { u.OrderNo, u.Code }).ToListAsync();
        var ids = list.Select(d => d.Id).Distinct().ToList();
        var translations = await _sysLangTextCacheService.GetTranslations(
                               "SysDictType",
                               "Name",
                               ids,
                               langCode);
        foreach (var item in list)
        {
            if (translations.TryGetValue(item.Id, out var translatedName) && !string.IsNullOrEmpty(translatedName))
            {
                item.Name = translatedName;
            }
        }
        return list;
    }

    /// <summary>
    /// 获取字典类型-值列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取字典类型-值列表")]
    public async Task<List<SysDictData>> GetDataList([FromQuery] GetDataDictTypeInput input)
    {
        var dictType = await _repository.GetFirstAsync(u => u.Code == input.Code) ?? throw Oops.Oh(ErrorCodeEnum.D3000);
        var langCode = _userManager.LangCode;
        var list = await _sysDictDataService.GetDictDataListByDictTypeId(dictType.Id);
        var ids = list.Select(d => d.Id).Distinct().ToList();
        var translations = await _sysLangTextCacheService.GetTranslations(
                               "SysDictType",
                               "Name",
                               ids,
                               langCode);
        foreach (var item in list)
        {
            if (translations.TryGetValue(item.Id, out var translatedName) && !string.IsNullOrEmpty(translatedName))
            {
                item.Name = translatedName;
            }
        }
        return list;
    }

    /// <summary>
    /// 添加字典类型 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("添加字典类型")]
    public async Task AddDictType(AddDictTypeInput input)
    {
        if (input.Code.ToLower().EndsWith("enum")) throw Oops.Oh(ErrorCodeEnum.D3006);
        if (input.SysFlag == YesNoEnum.Y && !_userManager.SuperAdmin) throw Oops.Oh(ErrorCodeEnum.D3008);

        var isExist = await _repository.IsAnyAsync(u => u.Code == input.Code);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D3001);

        if (_userManager.SuperAdmin) input.IsTenant = YesNoEnum.N;  // 超级管理员添加的字典类型默认非租户级

        await _repository.InsertAsync(input.Adapt<SysDictType>());
    }

    /// <summary>
    /// 更新字典类型 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新字典类型")]
    public async Task UpdateDictType(UpdateDictTypeInput input)
    {
        var dict = await _repository.GetFirstAsync(x => x.Id == input.Id);
        if (dict.IsTenant != input.IsTenant) throw Oops.Oh(ErrorCodeEnum.D3012);
        if (dict == null) throw Oops.Oh(ErrorCodeEnum.D3000);

        if (dict.Code.ToLower().EndsWith("enum") && input.Code != dict.Code) throw Oops.Oh(ErrorCodeEnum.D3007);
        if (input.SysFlag == YesNoEnum.Y && !_userManager.SuperAdmin) throw Oops.Oh(ErrorCodeEnum.D3009);

        var isExist = await _repository.IsAnyAsync(u => u.Code == input.Code && u.Id != input.Id);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D3001);

        _cacheManager.Remove($"{CacheConst.KeyDict}{input.Code}");
        await _repository.UpdateAsync(input.Adapt<SysDictType>());
    }

    /// <summary>
    /// 删除字典类型 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除字典类型")]
    public async Task DeleteDictType(DeleteDictTypeInput input)
    {
        var dictType = await _repository.GetByIdAsync(input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D3000);
        if (dictType.SysFlag == YesNoEnum.Y && !_userManager.SuperAdmin) throw Oops.Oh(ErrorCodeEnum.D3010);

        // 删除字典值
        await _repository.DeleteAsync(dictType);
        await _sysDictDataService.DeleteDictData(input.Id);
    }

    /// <summary>
    /// 获取字典类型详情 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取字典类型详情")]
    public async Task<SysDictType> GetDetail([FromQuery] DictTypeInput input)
    {
        return await _repository.GetByIdAsync(input.Id);
    }

    /// <summary>
    /// 修改字典类型状态 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [DisplayName("修改字典类型状态")]
    public async Task SetStatus(DictTypeInput input)
    {
        var dictType = await _repository.GetByIdAsync(input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D3000);
        if (dictType.SysFlag == YesNoEnum.Y && !_userManager.SuperAdmin) throw Oops.Oh(ErrorCodeEnum.D3009);

        _cacheManager.Remove($"{CacheConst.KeyDict}{dictType.Code}");

        dictType.Status = input.Status;
        await _repository.AsUpdateable(dictType).UpdateColumns(u => new { u.Status }, true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取所有字典集合 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取所有字典集合")]
    public async Task<dynamic> GetAllDictList()
    {
        var langCode = _userManager.LangCode;
        var ds = await _repository.AsQueryable()
            .InnerJoin(_sysDictDataQuery, (u, w) => u.Id == w.DictTypeId)
            .Select((u, w) => new DictDataOutput
            {
                DictDataId = w.Id, // 给翻译用
                TypeCode = u.Code,
                Label = w.Label,
                Value = w.Value,
                Code = w.Code,
                TagType = w.TagType,
                StyleSetting = w.StyleSetting,
                ClassSetting = w.ClassSetting,
                ExtData = w.ExtData,
                Remark = w.Remark,
                OrderNo = w.OrderNo,
                Status = w.Status == StatusEnum.Enable && u.Status == StatusEnum.Enable ? StatusEnum.Enable : StatusEnum.Disable
            })
            .ToListAsync();
        var ids = ds.Select(x => x.DictDataId).Distinct().ToList();

        Dictionary<long, string> translations = new();
        if (ids.Any())
        {
            translations = await _sysLangTextCacheService.GetTranslations(
                "SysDictData",
                "Label",
                ids,
                langCode
            );
        }
        foreach (var item in ds)
        {
            if (translations.TryGetValue(item.DictDataId, out var translated) && !string.IsNullOrEmpty(translated))
            {
                item.Label = translated;
            }
        }

        var result = ds
            .OrderBy(u => u.OrderNo)
            .GroupBy(u => u.TypeCode)
            .ToDictionary(u => u.Key, u => u.ToList());

        return result;
    }
}