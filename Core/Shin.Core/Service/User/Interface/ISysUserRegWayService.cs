// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统用户注册方案服务接口
/// </summary>
public interface ISysUserRegWayService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 查询注册方案列表
    /// </summary>
    Task<List<UserRegWayOutput>> List(PageUserRegWayInput input);

    /// <summary>
    /// 增加注册方案
    /// </summary>
    Task<long> Add(AddUserRegWayInput input);

    /// <summary>
    /// 更新注册方案
    /// </summary>
    Task Update(UpdateUserRegWayInput input);

    /// <summary>
    /// 检查数据
    /// </summary>
    Task CheckData(AddUserRegWayInput input);

    /// <summary>
    /// 删除注册方案
    /// </summary>
    Task Delete(BaseIdInput input);
}