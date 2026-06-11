// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Furion;
using Furion.Authorization;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shin.Core;
using Shin.Core.Interface;
using System;
using System.Threading.Tasks;

namespace Shin.Web.Core;

public class JwtHandler : AppAuthorizeHandler
{
    private readonly ICacheManager _cacheManager = App.GetRequiredService<ICacheManager>();
    private readonly ISysConfigService _sysConfigService = App.GetRequiredService<ISysConfigService>();
    private static readonly ISysMenuService SysMenuService = App.GetRequiredService<ISysMenuService>();

    /// <summary>
    /// 自动刷新Token
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override async Task HandleAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        var userId = context.User.FindFirst(ClaimConst.UserId)?.Value;
        var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // 🛡️ 黑名单校验（包括用户和token）
        if (_cacheManager.ExistKey($"{CacheConst.KeyBlacklist}{userId}") ||
            _cacheManager.ExistKey($"blacklist:token:{token}"))
        {
            context.Fail();
            context.GetCurrentHttpContext().SignoutToSwagger();
            return;
        }

        var tokenExpire = await _sysConfigService.GetTokenExpire();
        var refreshTokenExpire = await _sysConfigService.GetRefreshTokenExpire();
        if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext(), tokenExpire, refreshTokenExpire))
        {
            await AuthorizeHandleAsync(context);
        }
        else
        {
            context.Fail(); // 授权失败
            var currentHttpContext = context.GetCurrentHttpContext();
            if (currentHttpContext == null) return;

            // 跳过由于 SignatureAuthentication 引发的失败
            if (currentHttpContext.Items.ContainsKey(SignatureAuthenticationDefaults.AuthenticateFailMsgKey)) return;
            currentHttpContext.SignoutToSwagger();
        }
    }

    public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 已自动验证 Jwt Token 有效性
        return await CheckAuthorizeAsync(httpContext);
    }

    /// <summary>
    /// 权限校验核心逻辑
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static async Task<bool> CheckAuthorizeAsync(DefaultHttpContext httpContext)
    {
        // 登录模式判断PC、APP
        if (App.User.FindFirst(ClaimConst.LoginMode)?.Value == ((int)LoginModeEnum.APP).ToString())
            return true;

        // 排除超管
        if (App.User.FindFirst(ClaimConst.AccountType)?.Value == ((int)AccountTypeEnum.SuperAdmin).ToString())
            return true;

        // 路由名称
        var routeName = httpContext.Request.Path.StartsWithSegments("/api")
            ? httpContext.Request.Path.Value![5..].Replace("/", ":")
            : httpContext.Request.Path.Value![1..].Replace("/", ":");

        // 获取用户拥有按钮权限集合
        var ownBtnPermList = await SysMenuService.GetOwnBtnPermList();
        if (ownBtnPermList.Exists(u => routeName.Equals(u, StringComparison.CurrentCultureIgnoreCase)))
            return true;

        // 获取系统所有按钮权限集合
        var allBtnPermList = await SysMenuService.GetAllBtnPermList();
        return allBtnPermList.TrueForAll(u => !routeName.Equals(u, StringComparison.CurrentCultureIgnoreCase));
    }
}