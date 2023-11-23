using BiznewWebUI.Data;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BiznewWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;


        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Article> article= _context.Articles
                .Where(q => q.IsDeleted == false)
                .Include(w=>w.AdvortsArticles)
                .ThenInclude(e=>e.Adevorts)
              
               
                .Include(y=>y.ArticleTags)
                .ThenInclude(u=>u.Tags)
                .ToList();
            ViewData["Advorts"] = _context.Advorts.ToList();
            return View(article);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}