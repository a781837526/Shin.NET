// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统通知公告服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 380, Description = "通知公告")]
public class SysNoticeService : ISysNoticeService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysNotice> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 系统在线用户服务
    /// </summary>
    private readonly ISysOnlineUserService _sysOnlineUserService;

    /// <summary>
    /// 初始化<see cref="SysNoticeService"/>类的新实例
    /// </summary>
    public SysNoticeService(ISqlSugarRepository<SysNotice> repository,
        IUserManager userManager,
        ISysOnlineUserService sysOnlineUserService)
    {
        _repository = repository;
        _userManager = userManager;
        _sysOnlineUserService = sysOnlineUserService;
    }

    /// <summary>
    /// 获取通知公告分页列表 📢
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取通知公告分页列表")]
    public async Task<SqlSugarPagedList<SysNotice>> Page(PageNoticeInput input)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(input.Title), u => u.Title.Contains(input.Title.Trim()))
            .WhereIF(input.Type > 0, u => u.Type == input.Type)
            .WhereIF(!_userManager.SuperAdmin, u => u.CreateUserId == _userManager.UserId)
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 增加通知公告 📢
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加通知公告")]
    public async Task AddNotice(AddNoticeInput input)
    {
        var notice = input.Adapt<SysNotice>();
        InitNoticeInfo(notice);
        await _repository.InsertAsync(notice);
    }

    /// <summary>
    /// 更新通知公告 📢
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新通知公告")]
    public async Task UpdateNotice(UpdateNoticeInput input)
    {
        if (input.CreateUserId != _userManager.UserId)
            throw Oops.Oh(ErrorCodeEnum.D7003);

        var notice = input.Adapt<SysNotice>();
        InitNoticeInfo(notice);
        await _repository.UpdateAsync(notice);
    }

    /// <summary>
    /// 删除通知公告 📢
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [UnitOfWork]
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除通知公告")]
    public async Task DeleteNotice(DeleteNoticeInput input)
    {
        var sysNotice = await _repository.GetByIdAsync(input.Id);

        if (sysNotice.CreateUserId != _userManager.UserId) throw Oops.Oh(ErrorCodeEnum.D7003);

        if (sysNotice.Status == NoticeStatusEnum.PUBLIC) throw Oops.Oh(ErrorCodeEnum.D7001);

        await _repository.DeleteAsync(u => u.Id == input.Id);

        await _repository.Context.Deleteable<SysNoticeUser>(u => u.NoticeId == input.Id).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 发布通知公告 📢
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("发布通知公告")]
    public async Task Public(NoticeInput input)
    {
        if (!(await _repository.IsAnyAsync(u => u.Id == input.Id && u.CreateUserId == _userManager.UserId)))
            throw Oops.Oh(ErrorCodeEnum.D7003);

        // 更新发布状态和时间
        await _repository.UpdateAsync(u => new SysNotice() { Status = NoticeStatusEnum.PUBLIC, PublicTime = DateTime.Now }, u => u.Id == input.Id);

        var notice = await _repository.GetByIdAsync(input.Id);

        // 通知到的人(所有账号)
        var userIdList = await _repository.Context.Queryable<SysUser>().Select(u => u.Id).ToListAsync();

        await _repository.Context.Deleteable<SysNoticeUser>(u => u.NoticeId == notice.Id).ExecuteCommandHasChangeAsync();
        var noticeUserList = userIdList.Select(u => new SysNoticeUser
        {
            NoticeId = notice.Id,
            UserId = u,
        }).ToList();
        await _repository.Context.Insertable(noticeUserList).ExecuteCommandAsync();

        // 广播所有在线账号
        await _sysOnlineUserService.PublicNotice(notice, userIdList);
    }

    /// <summary>
    /// 设置通知公告已读状态 📢
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("设置通知公告已读状态")]
    public async Task SetRead(NoticeInput input)
    {
        await _repository.Context.Updateable<SysNoticeUser>(u => new SysNoticeUser
        {
            ReadStatus = NoticeUserStatusEnum.READ,
            ReadTime = DateTime.Now
        })
            .Where(u => u.NoticeId == input.Id && u.UserId == _userManager.UserId)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 获取接收的通知公告
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取接收的通知公告")]
    public async Task<SqlSugarPagedList<SysNoticeUser>> PageReceived(PageNoticeInput input)
    {
        return await _repository.Context.Queryable<SysNoticeUser>().Includes(u => u.SysNotice)
            .Where(u => u.UserId == _userManager.UserId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.Title), u => u.SysNotice.Title.Contains(input.Title.Trim()))
            .WhereIF(input.Type is > 0, u => u.SysNotice.Type == input.Type)
            .OrderBy(u => u.SysNotice.CreateTime, OrderByType.Desc)
            .ToPagedListAsync(input.Page, input.PageSize);
    }

    /// <summary>
    /// 获取未读的通知公告 📢
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取未读的通知公告")]
    public async Task<List<SysNotice>> GetUnReadList()
    {
        var noticeUserList = await _repository.Context.Queryable<SysNoticeUser>().Includes(u => u.SysNotice)
            .Where(u => u.UserId == _userManager.UserId && u.ReadStatus == NoticeUserStatusEnum.UNREAD)
            .OrderBy(u => u.SysNotice.CreateTime, OrderByType.Desc).ToListAsync();
        return noticeUserList.Select(t => t.SysNotice).ToList();
    }

    /// <summary>
    /// 初始化通知公告信息
    /// </summary>
    /// <param name="notice"></param>
    [NonAction]
    private void InitNoticeInfo(SysNotice notice)
    {
        notice.PublicUserId = _userManager.UserId;
        notice.PublicUserName = _userManager.RealName;
        notice.PublicOrgId = _userManager.OrgId;
    }
}