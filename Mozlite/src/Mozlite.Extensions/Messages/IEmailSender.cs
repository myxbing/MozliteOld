using System.Threading.Tasks;
using Mozlite.Core;

namespace Mozlite.Extensions.Messages
{
    /// <summary>
    /// 电子邮件发送接口。
    /// </summary>
    public interface IEmailSender : ISingletonService
    {
        /// <summary>
        /// 发送电子邮件。
        /// </summary>
        /// <param name="email">邮件地址。</param>
        /// <param name="subject">标题。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}