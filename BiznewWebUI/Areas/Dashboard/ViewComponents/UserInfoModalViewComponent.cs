using BiznewWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BiznewWebUI.Areas.Dashboard.ViewComponents
{
    public class UserInfoModalViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserInfoModalViewComponent(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            return View("UserInfoModal", user);

        }
    }
}
