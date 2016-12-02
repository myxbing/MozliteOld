using System.Threading.Tasks;

namespace Mozlite.Extensions.Messages
{
    /// <summary>
    /// 消息发送实现类。
    /// </summary>
    public class MessageSender : IEmailSender, ISmsSender
    {
        /// <summary>
        /// 发送电子邮件。
        /// </summary>
        /// <param name="email">邮件地址。</param>
        /// <param name="subject">标题。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
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
    }
}