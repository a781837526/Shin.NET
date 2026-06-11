// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统职位服务接口
/// </summary>
public interface ISysPosService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取职位列表
    /// </summary>
    Task<List<SysPos>> GetList(PosInput input);

    /// <summary>
    /// 增加职位
    /// </summary>
    Task AddPos(AddPosInput input);

    /// <summary>
    /// 更新职位
    /// </summary>
    Task UpdatePos(UpdatePosInput input);

    /// <summary>
    /// 删除职位
    /// </summary>
    Task DeletePos(DeletePosInput input);
}