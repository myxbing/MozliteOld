namespace Mozlite.Mvc.Messages
{
    /// <summary>
    /// 消息类型。
    /// </summary>
    public class BsMessage
    {
        /// <summary>
        /// 初始化类<see cref="BsMessage"/>。
        /// </summary>
        /// <param name="message">消息。</param>
        /// <param name="type">消息类型。</param>
        public BsMessage(string message, BsType? type = null)
        {
            Type = type;
            Message = message;
        }

        /// <summary>
        /// 消息类型。
        /// </summary>
        public BsType? Type { get; }

        /// <summary>
        /// 消息。
        /// </summary>
        public string Message { get; }
    }
}