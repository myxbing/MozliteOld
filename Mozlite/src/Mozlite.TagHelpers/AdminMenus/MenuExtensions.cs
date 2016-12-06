using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Mozlite.Mvc.AdminMenus;

namespace Mozlite.TagHelpers.AdminMenus
{
    internal static class MenuExtensions
    {
        private const string CurrentKey = "Current";
        private const string CurrentCacheKey = "CurrentNav";
        public static MenuItem GetCurrent(this ViewContext viewContext, IMenuProviderFactory factory, string provider, IUrlHelper urlHelper)
        {
            var current = viewContext.HttpContext.Items[CurrentCacheKey] as MenuItem;
            if (current == null)
            {
                var name = viewContext.ViewData[CurrentKey]?.ToString();
                if (name != null)
                {
                    var item = factory.GetMenu(provider, name);
                    if (item != null)
                        return item;
                }
                var path = viewContext.HttpContext.Request.Path;
                current = factory.GetMenus(provider).FirstOrDefault(it => string.Compare(it.LinkUrl(urlHelper, null), path, true) == 0);
                viewContext.HttpContext.Items[CurrentCacheKey] = current;
            }
            return current;
        }

        public static bool IsInRoles(this ViewContext viewContext, MenuItem item)
        {
            if (item.Roles.Length == 0)
                return true;
            return item.Roles.Any(it => viewContext.HttpContext.User.IsInRole(it));
        }

        public static bool IsCurrent(this MenuItem current, MenuItem item)
        {
            while (current != null && current.Level >= 0)
            {
                if (current.Name == item.Name)
                    return true;
                current = current.Parent;
            }
            return false;
        }

        public static MenuItem GetTopMenu(this MenuItem current)
        {
            while (current != null && current.Level > 0)
            {
                current = current.Parent;
            }
            return current;
        }
    }
}