using Microsoft.AspNetCore.Mvc;

namespace Mozlite.Extensions.Security.Controllers
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Area(SecuritySettings.ExtensionName)]
    public abstract class ControllerBase : Mvc.Controllers.ControllerBase
    {

    }

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Area(SecuritySettings.ExtensionName)]
    public abstract class AdminControllerBase : Mvc.Controllers.AdminControllerBase
    {

    }

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Area(SecuritySettings.ExtensionName)]
    public abstract class UserControllerBase : Mvc.Controllers.UserControllerBase
    {

    }
}