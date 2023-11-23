using BiznewWebUI.Data;
using BiznewWebUI.Helper;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _env;
        public ArticleController(AppDbContext context, UserManager<User> userManager, IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _env = env;
        }

        public IActionResult index()
        {
            var article = _context.Articles
                .Where(x => x.IsDeleted == false)
                .Include(a => a.User)
                .Include(b => b.AdvortsArticles)
                .ThenInclude(c => c.Adevorts)
                .Include(d => d.Category)
                .Include(e => e.ArticleTags)
                .ThenInclude(f => f.Tags)





               .ToList();





            return View(article);

        }
        [HttpGet]
        public IActionResult Create()
        {

            var tags = _context.Tags.ToList();
            var categories = _context.Categories.ToList();
            var advorts = _context.Advorts.ToList();
            ViewData["Categories"] = new SelectList(categories, "Id", "CategoryName");

            ViewData["Advorts"] = new SelectList(advorts, "Id", "AdvortName");
            ViewData["Tags"] = tags;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article article, IFormFile photo, List<Guid> tagsId, List<string> advortsId)
        {
            try
            {
                var currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User currentUserInfo = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
                if (tagsId.Count == 0)
                {
                    ViewData["ErrorTags"] = "Tag is Empty1";
                    return View();
                }

                Guid guid = Guid.NewGuid();
                Article newArticle = new Article()
                {

                    CategoryId = article.CategoryId,
                    Title = article.Title,
                    Content = article.Content,
                    CreatedDate = DateTime.Now,
                    CreatedBy = currentUserInfo.UserName,
                    UserId = currentUserId,
                    IsFeatured = article.IsFeatured,
                    IsPublished = article.IsPublished,
                    PhotoUrl = await FileHelper.SaveFileAsync(photo, _env.WebRootPath),
                    SeoUrl = SeoHelper.ReplaceInvalidChars(article.Title),



                };
                await _context.Articles.AddAsync(newArticle);

                if (advortsId.Count != 0)
                {

                    List<AdvortsArticle> advortslist = new List<AdvortsArticle>();
                    for (int i = 0; i < advortsId.Count; i++)
                    {
                        AdvortsArticle newAdAr = new()
                        {

                            AdevortId = advortsId[i],
                            ArticleId = newArticle.Id.ToString(),
                        };
                        advortslist.Add(newAdAr);

                    }
                    await _context.AdvortsArticles.AddRangeAsync(advortslist);
                }


                List<ArticleTag> tagList = new();

                for (int i = 0; i < tagsId.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {

                        ArticleId = newArticle.Id,
                        TagId = tagsId[i]

                    };
                    tagList.Add(articleTag);
                }

                await _context.ArticleTags.AddRangeAsync(tagList);
                Models.UserActions newAction = new Models.UserActions
                {
                    ActionName = "Create",
                    Article = newArticle,
                    DateTime = DateTime.Now,
                 
                    userId = currentUserId,
                    IsActive = true

                };
                await _context.UserActions.AddAsync(newAction);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return NotFound();
            }

        }
        public async Task<IActionResult> Delete(string articleId)
        {
            if (string.IsNullOrEmpty(articleId))
            {
                ViewData["ArticleDeleted"] = "Article Id is emtyp!";
                return RedirectToAction("index");
            }
            string currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User curretnuser = _userManager.Users.FirstOrDefault(x => x.Id == currentUserId);
            Article article = await _context.Articles.FirstOrDefaultAsync(x => x.Id.ToString() == articleId);
            article.IsDeleted = true;




            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }


        public async Task<IActionResult> Edit(string articleId)
        {
            if (string.IsNullOrEmpty(articleId)) { return NotFound(); }
            Article article = await _context.Articles.
                Include(x => x.ArticleTags)
                .ThenInclude(y => y.Tags)
                .Include(z => z.Category)

                .FirstOrDefaultAsync(x => x.Id.ToString() == articleId);

            if (article == null) return NotFound();

            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewData["tags"] = tags;

            return View(article);

        }
        [HttpPost]
        public async Task<ActionResult> Edit(Article article, IFormFile Photo, List<Guid> tagIds)
        {
            try
            {
                string currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == currentUserId);
                var categories = _context.Categories.ToList();
                var tags = _context.Tags.ToList();
                ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
                ViewData["tags"] = tags;

                if (tagIds.Count == 0)
                {
                    ViewBag.TagError = "Please select tag!";
                    return View();
                }

                if (Photo != null)
                {
                    article.PhotoUrl = await Photo.SaveFileAsync(_env.WebRootPath);
                }



                article.SeoUrl = SeoHelper.ReplaceInvalidChars(article.Title);

                article.UpdateDate = DateTime.Now;
                var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var user = await _userManager.FindByIdAsync(userId);
                article.UpdatedUserId = userId;
                var articleTagDelete = _context.ArticleTags.Where(x => x.ArticleId == article.Id).ToList();
                _context.ArticleTags.RemoveRange(articleTagDelete);

                List<ArticleTag> tagList = new();
                for (int i = 0; i < tagIds.Count; i++)
                {
                    ArticleTag articleTag = new()
                    {
                        ArticleId = article.Id,
                        TagId = tagIds[i]
                    };
                    tagList.Add(articleTag);
                }
                _context.ArticleTags.UpdateRange(tagList);
                await _context.SaveChangesAsync();

                _context.Articles.Update(article);
                Models.UserActions newAction = new Models.UserActions
                {
                    ActionName = "Create",
                    Article = article,
                    DateTime = DateTime.Now,
             
                    userId = currentUserId,
                    IsActive = true

                };
                await _context.UserActions.AddAsync(newAction);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return NotFound();
            }


        }
    }
}
