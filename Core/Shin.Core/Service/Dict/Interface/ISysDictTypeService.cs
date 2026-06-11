// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统字典类型服务接口
/// </summary>
public interface ISysDictTypeService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取字典类型分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysDictType>> Page(PageDictTypeInput input);

    /// <summary>
    /// 获取字典类型列表
    /// </summary>
    Task<List<SysDictType>> GetList();

    /// <summary>
    /// 获取字典类型-值列表
    /// </summary>
    Task<List<SysDictData>> GetDataList([FromQuery] GetDataDictTypeInput input);

    /// <summary>
    /// 添加字典类型
    /// </summary>
    Task AddDictType(AddDictTypeInput input);

    /// <summary>
    /// 更新字典类型
    /// </summary>
    Task UpdateDictType(UpdateDictTypeInput input);

    /// <summary>
    /// 删除字典类型
    /// </summary>
    Task DeleteDictType(DeleteDictTypeInput input);

    /// <summary>
    /// 获取字典类型详情
    /// </summary>
    Task<SysDictType> GetDetail(DictTypeInput input);

    /// <summary>
    /// 修改字典类型状态
    /// </summary>
    Task SetStatus(DictTypeInput input);

    /// <summary>
    /// 获取所有字典集合
    /// </summary>
    Task<dynamic> GetAllDictList();
}