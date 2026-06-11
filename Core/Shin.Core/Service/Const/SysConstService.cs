// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统常量服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 280)]
public class SysConstService : ISysConstService
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    private readonly ICacheManager _cacheManager;

    /// <summary>
    /// 初始化<see cref="SysConstService"/>类的新实例
    /// </summary>
    public SysConstService(ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
    }

    /// <summary>
    /// 获取所有常量列表 🔖
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取所有常量列表")]
    public async Task<List<ConstOutput>> GetList()
    {
        var key = $"{CacheConst.KeyConst}list";
        var constList = _cacheManager.Get<List<ConstOutput>>(key);
        if (constList == null)
        {
            var typeList = GetConstAttributeList();
            constList = typeList.Select(u => new ConstOutput
            {
                Name = u.CustomAttributes.ToList().FirstOrDefault()?.ConstructorArguments.ToList().FirstOrDefault().Value?.ToString() ?? u.Name,
                Code = u.Name,
                Data = GetData(Convert.ToString(u.Name))
            }).ToList();
            _cacheManager.Set(key, constList);
        }
        return await Task.FromResult(constList);
    }

    /// <summary>
    /// 根据类名获取常量数据 🔖
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    [DisplayName("根据类名获取常量数据")]
    public async Task<List<ConstOutput>> GetData([Required] string typeName)
    {
        var key = $"{CacheConst.KeyConst}{typeName.ToUpper()}";
        var constList = _cacheManager.Get<List<ConstOutput>>(key);
        if (constList == null)
        {
            var typeList = GetConstAttributeList();
            var type = typeList.FirstOrDefault(u => u.Name == typeName);
            if (type != null)
            {
                var isEnum = type.BaseType!.Name == "Enum";
                constList = type.GetFields()?
                    .Where(isEnum, u => u.FieldType.Name == typeName)
                    .Select(u => new ConstOutput
                    {
                        Name = u.Name,
                        Code = isEnum ? (int)u.GetValue(BindingFlags.Instance)! : u.GetValue(BindingFlags.Instance)
                    }).ToList();
                _cacheManager.Set(key, constList);
            }
        }
        return await Task.FromResult(constList);
    }

    #region Private

    /// <summary>
    /// 获取常量特性类型列表
    /// </summary>
    /// <returns></returns>
    private List<Type> GetConstAttributeList()
    {
        return App.EffectiveTypes.Where(u => u.CustomAttributes.Any(c => c.AttributeType == typeof(ConstAttribute))).ToList();
    }

    #endregion Private
}