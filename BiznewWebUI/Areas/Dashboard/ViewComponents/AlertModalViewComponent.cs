using BiznewWebUI.Data;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiznewWebUI.Areas.Dashboard.ViewComponents
{
    public class AlertModalViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AlertModalViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var alertActions = _context.UserActions.Where(x => x.IsActive == true)
               
               .Include(b => b.Advort)
               .Include(c => c.Article)
               .Include(d => d.Category)
               .Include(e => e.Tag)
             .ToList();
            
            var currentUrl = Request.GetDisplayUrl().Split("/");
            int dashboardIndex = -1;

            for (int i = 0; i < currentUrl.Length; i++)
            {
                if (currentUrl[i].Contains("dashboard"))
                {
                    dashboardIndex = i;
                    ViewBag.currentUrl = string.Join("/", currentUrl.Skip(dashboardIndex));
                    break;
                }

            }






            return View("AlertModal", alertActions);
        }
    }
}
