using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
        [Area(nameof(Dashboard))]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["ErrorRole"] is not null)
            {
                
            string errorMessage = TempData["ErrorRole"].ToString();
            ViewBag.ErrorMessage = errorMessage;
            }
            return View();
        }
       
    }
}
