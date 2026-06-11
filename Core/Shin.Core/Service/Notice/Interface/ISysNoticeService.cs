// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统通知公告服务接口
/// </summary>
public interface ISysNoticeService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取通知公告分页列表
    /// </summary>
    Task<SqlSugarPagedList<SysNotice>> Page(PageNoticeInput input);

    /// <summary>
    /// 增加通知公告
    /// </summary>
    Task AddNotice(AddNoticeInput input);

    /// <summary>
    /// 更新通知公告
    /// </summary>
    Task UpdateNotice(UpdateNoticeInput input);

    /// <summary>
    /// 删除通知公告
    /// </summary>
    Task DeleteNotice(DeleteNoticeInput input);

    /// <summary>
    /// 发布通知公告
    /// </summary>
    Task Public(NoticeInput input);

    /// <summary>
    /// 设置通知公告已读状态
    /// </summary>
    Task SetRead(NoticeInput input);

    /// <summary>
    /// 获取接收的通知公告
    /// </summary>
    Task<SqlSugarPagedList<SysNoticeUser>> PageReceived(PageNoticeInput input);

    /// <summary>
    /// 获取未读的通知公告
    /// </summary>
    Task<List<SysNotice>> GetUnReadList();
}