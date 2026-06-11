// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统更新管理服务接口
/// </summary>
public interface ISysUpdateService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 备份列表
    /// </summary>
    Task<List<BackupOutput>> List();

    /// <summary>
    /// 还原
    /// </summary>
    Task Restore(RestoreInput input);

    /// <summary>
    /// 从远端更新系统
    /// </summary>
    Task Update();

    /// <summary>
    /// 仓库WebHook接口
    /// </summary>
    Task WebHook(Dictionary<string, object> input);

    /// <summary>
    /// 获取WebHook接口密钥
    /// </summary>
    string GetWebHookKey();

    /// <summary>
    /// 获取日志列表
    /// </summary>
    List<string> LogList();

    /// <summary>
    /// 清空日志
    /// </summary>
    void ClearLog();
}