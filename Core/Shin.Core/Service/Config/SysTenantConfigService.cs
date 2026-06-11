// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统租户配置参数服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 440)]
public class SysTenantConfigService : ISysTenantConfigService
{
    /// <summary>
    /// 系统配置参数表查询器
    /// </summary>
    private readonly ISugarQueryable<SysConfig> _sysConfigQuery;

    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysTenantConfig> _repository;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 初始化<see cref="SysTenantConfigService"/>类的新实例
    /// </summary>
    public SysTenantConfigService(ICacheManager cacheManager,
        IUserManager userManager,
        ISqlSugarRepository<SysTenantConfig> repository)
    {
        _userManager = userManager;
        _cacheManager = cacheManager;
        _repository = repository;
        _sysConfigQuery = _repository.AsQueryable()
            .InnerJoin(
                _repository.Context.Queryable<SysTenantConfigData>().WhereIF(!_userManager.SuperAdmin, cv => cv.TenantId == _userManager.TenantId),
                (c, cv) => c.Id == cv.ConfigId
            )
            //.Select<SysConfig>().MergeTable();
            // 解决PostgreSQL下并启用驼峰转下划线时,报字段不存在,SqlSugar在Select后生成的sql, as后没转下划线导致.
            .SelectMergeTable((c, cv) => new SysConfig { Id = c.Id.SelectAll(), Value = cv.Value });
    }

    /// <summary>
    /// 获取配置参数分页列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取配置参数分页列表")]
    public async Task<SqlSugarPagedList<SysConfig>> Page(PageConfigInput input)
    {
        return await _sysConfigQuery
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name?.Trim()), u => u.Name.Contains(input.Name))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code?.Trim()), u => u.Code.Contains(input.Code))
            .WhereIF(!string.IsNullOrWhiteSpace(input.GroupCode?.Trim()), u => u.GroupCode.Equals(input.GroupCode))
            .OrderBuilder(input)
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取配置参数列表 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取配置参数列表")]
    public async Task<List<SysConfig>> List(PageConfigInput input)
    {
        return await _sysConfigQuery
            .WhereIF(!string.IsNullOrWhiteSpace(input.GroupCode?.Trim()), u => u.GroupCode.Equals(input.GroupCode))
            .ToListAsync();
    }

    /// <summary>
    /// 增加配置参数 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加配置参数")]
    public async Task AddConfig(AddConfigInput input)
    {
        var isExist = await _repository.IsAnyAsync(u => u.Name == input.Name || u.Code == input.Code);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D9000);

        var configId = _repository.InsertReturnSnowflakeId(input.Adapt<SysTenantConfig>());
        await _repository.Context.Insertable(new SysTenantConfigData()
        {
            ConfigId = configId,
            Value = input.Value
        }).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新配置参数 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新配置参数")]
    [UnitOfWork]
    public async Task UpdateConfig(UpdateConfigInput input)
    {
        var isExist = await _repository.IsAnyAsync(u => (u.Name == input.Name || u.Code == input.Code) && u.Id != input.Id);
        if (isExist) throw Oops.Oh(ErrorCodeEnum.D9000);

        var config = input.Adapt<SysTenantConfig>();
        await _repository.AsUpdateable(config).IgnoreColumns(true).ExecuteCommandAsync();
        var configData = await _repository.Context.Queryable<SysTenantConfigData>().Where(cv => cv.ConfigId == input.Id).FirstAsync();
        if (configData == null)
            await _repository.Context.Insertable(new SysTenantConfigData() { ConfigId = input.Id, Value = input.Value }).ExecuteCommandAsync();
        else
        {
            configData.Value = input.Value;
            await _repository.Context.Updateable(configData).IgnoreColumns(true).ExecuteCommandAsync();
        }

        RemoveConfigCache(config);
    }

    /// <summary>
    /// 删除配置参数 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除配置参数")]
    public async Task DeleteConfig(DeleteConfigInput input)
    {
        var config = await _repository.GetByIdAsync(input.Id);
        // 禁止删除系统参数
        if (config.SysFlag == YesNoEnum.Y) throw Oops.Oh(ErrorCodeEnum.D9001);

        await _repository.DeleteAsync(config);

        List<SysTenantConfigData> delList = await _repository.Context.Queryable<SysTenantConfigData>().Where(it => it.TenantId == _userManager.TenantId && it.ConfigId == config.Id).ToListAsync();
        await _repository.Context.Deleteable(delList).ExecuteCommandHasChangeAsync();

        RemoveConfigCache(config);
    }

    /// <summary>
    /// 批量删除配置参数 🔖
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "BatchDelete"), HttpPost]
    [DisplayName("批量删除配置参数")]
    public async Task BatchDeleteConfig(List<long> ids)
    {
        foreach (var id in ids)
        {
            var config = await _repository.GetByIdAsync(id);
            // 禁止删除系统参数
            if (config.SysFlag == YesNoEnum.Y) continue;

            await _repository.DeleteAsync(config);
            List<SysTenantConfigData> delList = await _repository.Context.Queryable<SysTenantConfigData>().Where(it => it.TenantId == _userManager.TenantId && it.ConfigId == config.Id).ToListAsync();
            await _repository.Context.Deleteable(delList).ExecuteCommandHasChangeAsync();

            RemoveConfigCache(config);
        }
    }

    /// <summary>
    /// 获取配置参数详情 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取配置参数详情")]
    public async Task<SysConfig> GetDetail([FromQuery] ConfigInput input)
    {
        return await _sysConfigQuery.FirstAsync(u => u.Id == input.Id);
    }

    /// <summary>
    /// 根据Code获取配置参数 🔖
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [NonAction]
    public async Task<SysConfig> GetConfig(string code)
    {
        return await _sysConfigQuery.FirstAsync(u => u.Code == code);
    }

    /// <summary>
    /// 根据Code获取配置参数值 🔖
    /// </summary>
    /// <param name="code">编码</param>
    /// <returns></returns>
    [DisplayName("根据Code获取配置参数值")]
    public async Task<string> GetConfigValueByCode(string code)
    {
        return await GetConfigValueByCode<string>(code);
    }

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <param name="code">编码</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    [NonAction]
    public async Task<string> GetConfigValueByCode(string code, string defaultValue = default)
    {
        return await GetConfigValueByCode<string>(code, defaultValue);
    }

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="code">编码</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    [NonAction]
    public async Task<T> GetConfigValueByCode<T>(string code, T defaultValue = default)
    {
        return await GetConfigValueByCode<T>(code, _userManager.TenantId, defaultValue);
    }

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="code">编码</param>
    /// <param name="tenantId">租户Id</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    [NonAction]
    public async Task<T> GetConfigValueByCode<T>(string code, long tenantId, T defaultValue = default)
    {
        if (string.IsNullOrWhiteSpace(code)) return defaultValue;

        var value = _cacheManager.Get<string>($"{CacheConst.KeyTenantConfig}{tenantId}{code}");
        if (string.IsNullOrEmpty(value))
        {
            value = (await _sysConfigQuery.FirstAsync(u => u.Code == code))?.Value;
            _cacheManager.Set($"{CacheConst.KeyTenantConfig}{tenantId}{code}", value);
        }
        if (string.IsNullOrWhiteSpace(value)) return defaultValue;
        return (T)Convert.ChangeType(value, typeof(T));
    }

    /// <summary>
    /// 更新配置参数值
    /// </summary>
    /// <param name="code"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    [NonAction]
    public async Task UpdateConfigValue(string code, string value)
    {
        var config = await _repository.GetFirstAsync(u => u.Code == code);
        if (config == null) return;

        await _repository.Context.Updateable<SysTenantConfigData>().SetColumns(it => it.Value == value).Where(it => it.TenantId == _userManager.TenantId && it.ConfigId == config.Id).ExecuteCommandAsync();

        RemoveConfigCache(config);
    }

    /// <summary>
    /// 获取分组列表 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取分组列表")]
    public async Task<List<string>> GetGroupList()
    {
        return await _sysConfigQuery
            .GroupBy(u => u.GroupCode)
            .Select(u => u.GroupCode).ToListAsync();
    }

    /// <summary>
    /// 批量更新配置参数值 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "BatchUpdate"), HttpPost]
    [DisplayName("批量更新配置参数值")]
    public async Task BatchUpdateConfig(List<BatchConfigInput> input)
    {
        foreach (var config in input)
        {
            await UpdateConfigValue(config.Code, config.Value);
        }
    }

    /// <summary>
    /// 清除配置缓存
    /// </summary>
    /// <param name="config"></param>
    private void RemoveConfigCache(SysTenantConfig config)
    {
        _cacheManager.Remove($"{CacheConst.KeyTenantConfig}{_userManager.TenantId}{config.Code}");
    }
}