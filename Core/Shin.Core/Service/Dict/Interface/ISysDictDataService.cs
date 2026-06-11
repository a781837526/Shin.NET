// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统字典值服务接口
/// </summary>
public interface ISysDictDataService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取字典值分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysDictData>> Page(PageDictDataInput input);

    /// <summary>
    /// 获取字典值列表
    /// </summary>
    Task<List<SysDictData>> GetList(GetDataDictDataInput input);

    /// <summary>
    /// 增加字典值
    /// </summary>
    Task AddDictData(AddDictDataInput input);

    /// <summary>
    /// 更新字典值
    /// </summary>
    Task UpdateDictData(UpdateDictDataInput input);

    /// <summary>
    /// 删除字典值
    /// </summary>
    Task DeleteDictData(DeleteDictDataInput input);

    /// <summary>
    /// 获取字典值详情
    /// </summary>
    Task<SysDictData> GetDetail(DictDataInput input);

    /// <summary>
    /// 修改字典值状态
    /// </summary>
    Task SetStatus(DictDataInput input);

    /// <summary>
    /// 根据字典类型Id获取字典值集合
    /// </summary>
    Task<List<SysDictData>> GetDictDataListByDictTypeId(long dictTypeId);

    /// <summary>
    /// 根据字典类型编码获取字典值集合
    /// </summary>
    Task<List<SysDictData>> GetDataList(string code);

    /// <summary>
    /// 获取字典值集合
    /// </summary>
    Task<List<SysDictData>> GetDataListByIdOrCode(long? typeId, string code);

    /// <summary>
    /// 根据查询条件获取字典值集合
    /// </summary>
    Task<List<SysDictData>> GetDataList(QueryDictDataInput input);

    /// <summary>
    /// 根据字典类型Id删除字典值
    /// </summary>
    Task DeleteDictData(long dictTypeId);

    /// <summary>
    /// 通过字典数据Value查询显示文本Label
    /// 适用于列表中根据字典数据值找文本的子查询 _sysDictDataService.MapDictValueToLabel(() =>obj.Type, "org_type",obj);
    /// </summary>
    string MapDictValueToLabel<T>(Expression<Func<object>> mappingFiled, string dictTypeCode, T parameter);

    /// <summary>
    /// 通过字典数据显示文本Label查询Value
    /// 适用于列表数据导入根据字典数据文本找值的子查询 _sysDictDataService.MapDictLabelToValue(() => obj.Type, "org_type",obj);
    /// </summary>
    string MapDictLabelToValue<T>(Expression<Func<object>> mappingFiled, string dictTypeCode, T parameter);
}