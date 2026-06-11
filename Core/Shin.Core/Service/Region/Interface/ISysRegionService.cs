// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统行政区域服务接口
/// </summary>
public interface ISysRegionService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取行政区域分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysRegion>> Page(PageRegionInput input);

    /// <summary>
    /// 获取行政区域列表
    /// </summary>
    Task<List<SysRegion>> GetList(RegionInput input);

    /// <summary>
    /// 获取行政区域树
    /// </summary>
    Task<List<SysRegion>> GetTree();

    /// <summary>
    /// 增加行政区域
    /// </summary>
    Task<long> AddRegion(AddRegionInput input);

    /// <summary>
    /// 更新行政区域
    /// </summary>
    Task UpdateRegion(UpdateRegionInput input);

    /// <summary>
    /// 删除行政区域
    /// </summary>
    Task DeleteRegion(DeleteRegionInput input);

    /// <summary>
    /// 同步行政区域
    /// </summary>
    Task Sync();
}