// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Application.Service;

/// <summary>
/// 示例系统服务 🧩
/// </summary>
[ApiDescriptionSettings(ApplicationConst.GroupName, Order = 100)]
public class DemoService : IDemoService, IDynamicApiController, ITransient
{
    /// <summary>
    /// 当前用户管理
    /// </summary>
    private readonly UserManager _userManager;

    /// <summary>
    /// 初始化<see cref="DemoService"/>类的新实例
    /// </summary>
    public DemoService(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("helloWord")]
    public Task<string> HelloWord()
    {
        return Task.FromResult($"Hello word. {_userManager.Account}");
    }
}