using System.Threading.Tasks;
using Mozlite.Core;

namespace Mozlite.Extensions.Messages
{
    /// <summary>
    /// 消息发送接口。
    /// </summary>
    public interface ISmsSender : ISingletonService
    {
        /// <summary>
        /// 发送消息。
        /// </summary>
        /// <param name="number">电话号码。</param>
        /// <param name="message">消息。</param>
        /// <returns>返回发送任务。</returns>
        Task SendSmsAsync(string number, string message);
    }
}