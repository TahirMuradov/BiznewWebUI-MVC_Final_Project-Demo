using Microsoft.AspNetCore.Mvc;

namespace BiznewWebUI.Controllers
{
    public class SingleController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
