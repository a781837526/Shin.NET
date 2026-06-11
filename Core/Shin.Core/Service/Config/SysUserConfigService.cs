// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统用户配置参数服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 440)]
public class SysUserConfigService : ISysUserConfigService
{
    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysUserConfig> _repository;

    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 系统配置参数查询器
    /// </summary>
    private readonly ISugarQueryable<SysConfig> _sysConfigQuery;

    /// <summary>
    /// 初始化<see cref="SysUserConfigService"/>类的新实例
    /// </summary>
    public SysUserConfigService(IUserManager userManager,
        ICacheManager cacheManager,
        ISqlSugarRepository<SysUserConfig> sysConfigRep)
    {
        _userManager = userManager;
        _cacheManager = cacheManager;
        _repository = sysConfigRep;
        _sysConfigQuery = _repository.AsQueryable()
            .LeftJoin(
                _repository.Context.Queryable<SysUserConfigData>().WhereIF(_userManager.SuperAdmin, cv => cv.UserId == _userManager.UserId),
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

        var configId = _repository.InsertReturnSnowflakeId(input.Adapt<SysUserConfig>());
        await _repository.Context.Insertable(new SysUserConfigData()
        {
            UserId = _userManager.UserId,
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

        var config = input.Adapt<SysUserConfig>();
        await _repository.AsUpdateable(config).IgnoreColumns(true).ExecuteCommandAsync();
        var configData = await _repository.Context.Queryable<SysUserConfigData>().FirstAsync(cv => cv.ConfigId == input.Id);
        if (configData == null)
            await _repository.Context.Insertable(new SysUserConfigData() { UserId = _userManager.UserId, ConfigId = input.Id, Value = input.Value }).ExecuteCommandAsync();
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
        await _repository.Context.Deleteable<SysUserConfigData>().Where(it => it.UserId == _userManager.UserId && it.ConfigId == config.Id).ExecuteCommandHasChangeAsync();

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
            await _repository.Context.Deleteable<SysUserConfigData>().Where(it => it.UserId == _userManager.UserId && it.ConfigId == config.Id).ExecuteCommandHasChangeAsync();

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
        return await GetConfigValueByCode<T>(code, _userManager.UserId, defaultValue);
    }

    /// <summary>
    /// 获取配置参数值
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="code">编码</param>
    /// <param name="userId">用户Id</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    [NonAction]
    public async Task<T> GetConfigValueByCode<T>(string code, long userId, T defaultValue = default)
    {
        if (string.IsNullOrWhiteSpace(code)) return defaultValue;

        var value = _cacheManager.Get<string>($"{CacheConst.KeyUserConfig}{userId}{code}");
        if (string.IsNullOrEmpty(value))
        {
            value = (await _sysConfigQuery.FirstAsync(u => u.Code == code))?.Value;
            _cacheManager.Set($"{CacheConst.KeyUserConfig}{userId}{code}", value);
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

        var configData = new SysUserConfigData() { UserId = _userManager.UserId, ConfigId = config.Id, Value = value };
        await _repository.Context.Storageable(configData).WhereColumns(it => new[] { it.UserId, it.ConfigId }).ExecuteCommandAsync();

        RemoveConfigCache(config);
    }

    /// <summary>
    /// 获取分组列表 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取分组列表")]
    public async Task<List<string>> GetGroupList()
    {
        return await _repository.AsQueryable()
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
    private void RemoveConfigCache(SysUserConfig config)
    {
        _cacheManager.Remove($"{CacheConst.KeyUserConfig}{_userManager.UserId}{config.Code}");
    }
}