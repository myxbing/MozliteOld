using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;
using Mozlite.Extensions.Security;
using Mozlite.Extensions.Settings;

namespace Mozlite.Extensions.Messages
{
    /// <summary>
    /// 消息发送实现类。
    /// </summary>
    public class MessageSender : IEmailSender, ISmsSender
    {
        private readonly ISettingsManager _settingsManager;
        /// <summary>
        /// 初始化类<see cref="MessageSender"/>。
        /// </summary>
        /// <param name="settingsManager">设置管理接口。</param>
        public MessageSender(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        /// <summary>
        /// 发送消息。
        /// </summary>
        /// <param name="number">电话号码。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 发送电子邮件。
        /// </summary>
        /// <param name="email">邮件地址。</param>
        /// <param name="subject">标题。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return SendEmailAsync(to =>
            {
                to.Add(new MailboxAddress(email));
            }, subject, message);
        }

        /// <summary>
        /// 发送电子邮件。
        /// </summary>
        /// <param name="user">用户名称。</param>
        /// <param name="subject">标题。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        public Task SendEmailAsync(User user, string subject, string message)
        {
            return SendEmailAsync(to =>
            {
                to.Add(new MailboxAddress(user.NickName, user.Email));
            }, subject, message);
        }

        /// <summary>
        /// 发送电子邮件。
        /// </summary>
        /// <param name="users">用户名称。</param>
        /// <param name="subject">标题。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        public Task SendEmailAsync(IEnumerable<User> users, string subject, string message)
        {
            return SendEmailAsync(to =>
            {
                foreach (var user in users)
                {
                    if (!string.IsNullOrWhiteSpace(user.Email))
                        to.Add(new MailboxAddress(user.NickName, user.Email));
                }
            }, subject, message);
        }

        private async Task SendEmailAsync(Action<InternetAddressList> action, string subject, string content)
        {
            var settings = _settingsManager.GetSettings<EmailSettings>();
            if (!settings.Enabled)
                return;
            var site = _settingsManager.GetSettings<SiteSettings>();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(site.SiteName, settings.SmtpUserName));
            action(message.To);
            message.Subject = ReplaceKeyWords(site, subject);
            var html = new TextPart("html");
            html.Text = ReplaceKeyWords(site, content);
            message.Body = html;
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.ConnectAsync(settings.SmtpServer, settings.SmtpPort, settings.UseSsl);
                await client.AuthenticateAsync(settings.SmtpUserName, settings.SmtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private string ReplaceKeyWords(SiteSettings settings, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return message;
            message = message.Replace("[$site;]", settings.SiteName);
            var now = DateTime.Now;
            message = message.Replace("[$now;]", now.ToString("yyyy-MM-dd HH:mm"));
            message = message.Replace("[$today;]", now.ToString("yyyy-MM-dd"));
            message = message.Replace("[$time;]", now.ToString("HH:mm"));
            message = message.Replace("[$year;]", now.Year.ToString());
            message = message.Replace("[$month;]", now.Month.ToString());
            message = message.Replace("[$day;]", now.Day.ToString());
            message = message.Replace("[$week;]", now.DayOfWeek.ToString("D"));
            return message;
        }
    }
}