// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统职位服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 460)]
public class SysPosService : ISysPosService
{
    /// <summary>
    /// 基础仓储服务
    /// </summary>
    private readonly ISqlSugarRepository<SysPos> _repository;

    /// <summary>
    /// 当前登录用户
    /// </summary>
    private readonly IUserManager _userManager;

    /// <summary>
    /// 系统用户扩展机构服务
    /// </summary>
    private readonly ISysUserExtOrgService _sysUserExtOrgService;

    /// <summary>
    /// 初始化<see cref="SysPosService"/>类的新实例
    /// </summary>
    public SysPosService(ISqlSugarRepository<SysPos> repository,
        IUserManager userManager,
        ISysUserExtOrgService sysUserExtOrgService)
    {
        _userManager = userManager;
        _repository = repository;
        _sysUserExtOrgService = sysUserExtOrgService;
    }

    /// <summary>
    /// 获取职位列表 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [DisplayName("获取职位列表")]
    public async Task<List<SysPos>> GetList([FromQuery] PosInput input)
    {
        return await _repository.AsQueryable()
            .WhereIF(_userManager.SuperAdmin && input.TenantId > 0, u => u.TenantId == input.TenantId)
            .WhereIF(!string.IsNullOrWhiteSpace(input.Name), u => u.Name.Contains(input.Name))
            .WhereIF(!string.IsNullOrWhiteSpace(input.Code), u => u.Code.Contains(input.Code))
            .OrderBy(u => new { u.OrderNo, u.Id })
            .Mapper(u =>
            {
                u.UserList = _repository.Context.Queryable<SysUser>()
                    .Where(a => a.PosId == u.Id || SqlFunc.Subqueryable<SysUserExtOrg>()
                        .Where(t => a.Id == t.UserId && t.PosId == u.Id).Any())
                    .ToList();
            })
            .ToListAsync();
    }

    /// <summary>
    /// 增加职位 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add"), HttpPost]
    [DisplayName("增加职位")]
    public async Task AddPos(AddPosInput input)
    {
        if (await _repository.IsAnyAsync(u => u.Name == input.Name && u.Code == input.Code)) throw Oops.Oh(ErrorCodeEnum.D6000);

        await _repository.InsertAsync(input.Adapt<SysPos>());
    }

    /// <summary>
    /// 更新职位 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Update"), HttpPost]
    [DisplayName("更新职位")]
    public async Task UpdatePos(UpdatePosInput input)
    {
        if (await _repository.IsAnyAsync(u => u.Name == input.Name && u.Code == input.Code && u.Id != input.Id))
            throw Oops.Oh(ErrorCodeEnum.D6000);

        var sysPos = await _repository.GetByIdAsync(input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D6003);
        if (!_userManager.SuperAdmin && sysPos.CreateUserId != _userManager.UserId) throw Oops.Oh(ErrorCodeEnum.D6002);

        await _repository.AsUpdateable(input.Adapt<SysPos>()).IgnoreColumns(true).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除职位 🔖
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Delete"), HttpPost]
    [DisplayName("删除职位")]
    public async Task DeletePos(DeletePosInput input)
    {
        var sysPos = await _repository.GetFirstAsync(u => u.Id == input.Id) ?? throw Oops.Oh(ErrorCodeEnum.D6003);
        if (!_userManager.SuperAdmin && sysPos.CreateUserId != _userManager.UserId) throw Oops.Oh(ErrorCodeEnum.D6002);

        // 若职位有用户则禁止删除
        var hasPosEmp = await _repository.ChangeRepository<SqlSugarRepository<SysUser>>()
            .IsAnyAsync(u => u.PosId == input.Id);
        if (hasPosEmp) throw Oops.Oh(ErrorCodeEnum.D6001);

        // 若附属职位有用户则禁止删除
        var hasExtPosEmp = await _sysUserExtOrgService.HasUserPos(input.Id);
        if (hasExtPosEmp) throw Oops.Oh(ErrorCodeEnum.D6001);

        // 若有绑定注册方案则禁止删除
        var hasUserRegWay = await _repository.Context.Queryable<SysUserRegWay>().AnyAsync(u => u.PosId == input.Id);
        if (hasUserRegWay) throw Oops.Oh(ErrorCodeEnum.D6004);

        await _repository.DeleteAsync(u => u.Id == input.Id);
    }
}