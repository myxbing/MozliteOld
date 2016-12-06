namespace Mozlite.Extensions.Security.ViewModels
{
    /// <summary>
    /// 注册用户模型。
    /// </summary>
    public class RegisterUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}