using BiznewWebUI.Data;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
   
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        public IActionResult index()
        {
            var article=_context.Articles.Include(a=>a.User)
                .Include(b=>b.AdevortsArticles)
                .ThenInclude(c=>c.Adevorts)
                .Include(d=>d.Category)
                .Include(e=>e.ArticleTags)
                .ThenInclude(f=>f.Tags)
                .Include(g=>g.User).ToList();
                         
               
              

            return View(article);

        }
        public IActionResult create()
        {


            return View();
        }
    }
}
