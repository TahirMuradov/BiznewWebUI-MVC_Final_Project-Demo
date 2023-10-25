using Microsoft.AspNetCore.Mvc;

namespace BiznewWebUI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
