// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统作业任务服务接口
/// </summary>
public interface ISysJobService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取作业分页列表
    /// </summary>
    Task<SqlSugarPagedList<JobDetailOutput>> PageJobDetail(PageJobDetailInput input);

    /// <summary>
    /// 获取作业组名称集合
    /// </summary>
    Task<List<string>> ListJobGroup();

    /// <summary>
    /// 添加作业
    /// </summary>
    Task AddJobDetail(AddJobDetailInput input);

    /// <summary>
    /// 更新作业
    /// </summary>
    Task UpdateJobDetail(UpdateJobDetailInput input);

    /// <summary>
    /// 删除作业
    /// </summary>
    Task DeleteJobDetail(DeleteJobDetailInput input);

    /// <summary>
    /// 获取触发器列表
    /// </summary>
    Task<List<SysJobTrigger>> GetJobTriggerList(JobDetailInput input);

    /// <summary>
    /// 添加触发器
    /// </summary>
    Task AddJobTrigger(AddJobTriggerInput input);

    /// <summary>
    /// 更新触发器
    /// </summary>
    Task UpdateJobTrigger(UpdateJobTriggerInput input);

    /// <summary>
    /// 删除触发器
    /// </summary>
    Task DeleteJobTrigger(DeleteJobTriggerInput input);

    /// <summary>
    /// 暂停所有作业
    /// </summary>
    void PauseAllJob();

    /// <summary>
    /// 启动所有作业
    /// </summary>
    void StartAllJob();

    /// <summary>
    /// 暂停作业
    /// </summary>
    void PauseJob(JobDetailInput input);

    /// <summary>
    /// 启动作业
    /// </summary>
    void StartJob(JobDetailInput input);

    /// <summary>
    /// 取消作业
    /// </summary>
    void CancelJob(JobDetailInput input);

    /// <summary>
    /// 执行作业
    /// </summary>
    void RunJob(JobDetailInput input);

    /// <summary>
    /// 暂停触发器
    /// </summary>
    void PauseTrigger(JobTriggerInput input);

    /// <summary>
    /// 启动触发器
    /// </summary>
    void StartTrigger(JobTriggerInput input);

    /// <summary>
    /// 强制唤醒作业调度器
    /// </summary>
    void CancelSleep();

    /// <summary>
    /// 强制触发所有作业持久化
    /// </summary>
    void PersistAll();

    /// <summary>
    /// 获取集群列表
    /// </summary>
    Task<List<SysJobCluster>> GetJobClusterList();

    /// <summary>
    /// 获取作业触发器运行记录分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysJobTriggerRecord>> PageJobTriggerRecord(PageJobTriggerRecordInput input);

    /// <summary>
    /// 清空作业触发器运行记录
    /// </summary>
    void ClearJobTriggerRecord();

    /// <summary>
    /// 清空不保留的作业触发器运行记录
    /// </summary>
    Task ClearExpireJobTriggerRecord(SysJobTriggerRecord input);
}