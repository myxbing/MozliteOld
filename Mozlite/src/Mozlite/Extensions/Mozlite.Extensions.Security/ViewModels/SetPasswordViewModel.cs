namespace Mozlite.Extensions.Security.ViewModels
{
    /// <summary>
    /// 设置密码。
    /// </summary>
    public class SetPasswordViewModel
    {
        public int UserId { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}