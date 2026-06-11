// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using MailKit.Net.Smtp;
using MimeKit;

namespace Shin.Core;

/// <summary>
/// 系统邮件发送服务 🧩
/// </summary>
[ApiDescriptionSettings(Order = 370)]
public class SysEmailService : ISysEmailService
{
    /// <summary>
    /// 基础服务仓储
    /// </summary>
    private readonly ISqlSugarRepository<SysTenant> _repository;

    /// <summary>
    /// 邮件配置选项
    /// </summary>
    private readonly EmailOptions _emailOptions;

    /// <summary>
    /// 初始化<see cref="SysEmailService"/>类的新实例
    /// </summary>
    public SysEmailService(ISqlSugarRepository<SysTenant> repository, IOptions<EmailOptions> emailOptions)
    {
        _repository = repository;
        _emailOptions = emailOptions.Value;
    }

    /// <summary>
    /// 发送邮件 📧
    /// </summary>
    /// <param name="content"></param>
    /// <param name="title"></param>
    /// <param name="toEmail"></param>
    /// <returns></returns>
    [DisplayName("发送邮件")]
    public async Task SendEmail([Required] string content, string title = "", string toEmail = "")
    {
        long.TryParse(App.User?.FindFirst(ClaimConst.TenantId)?.Value ?? SqlSugarConst.DefaultTenantId.ToString(), out var tenantId);
        var webTitle = (await _repository.GetFirstAsync(u => u.Id == tenantId))?.Title;
        title = string.IsNullOrWhiteSpace(title) ? $"{webTitle} 系统邮件" : title;
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_emailOptions.DefaultFromEmail, _emailOptions.DefaultFromEmail));

        message.To.Add(string.IsNullOrWhiteSpace(toEmail)
            ? new MailboxAddress(_emailOptions.DefaultToEmail, _emailOptions.DefaultToEmail)
            : new MailboxAddress(toEmail, toEmail));

        message.Subject = title;
        message.Body = new TextPart("html")
        {
            Text = content
        };

        using var client = new SmtpClient();
        client.Connect(_emailOptions.Host, _emailOptions.Port, _emailOptions.EnableSsl);
        client.Authenticate(_emailOptions.UserName, _emailOptions.Password);
        client.Send(message);
        client.Disconnect(true);

        await Task.CompletedTask;
    }
}