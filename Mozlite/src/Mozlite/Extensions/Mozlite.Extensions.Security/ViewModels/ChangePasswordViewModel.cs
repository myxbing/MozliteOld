namespace Mozlite.Extensions.Security.ViewModels
{
    /// <summary>
    /// 修改密码试图模型。
    /// </summary>
    public class ChangePasswordViewModel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}