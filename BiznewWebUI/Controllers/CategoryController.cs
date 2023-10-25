using Microsoft.AspNetCore.Mvc;

namespace BiznewWebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
