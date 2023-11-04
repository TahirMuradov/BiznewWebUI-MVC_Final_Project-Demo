using BiznewWebUI.Data;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class CategoryController : Controller
    {
       private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CategoryController(AppDbContext context, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            var category=_context.Categories
                .Include(a=>a.User)
                .ToList();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {


            var checkCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(category.CategoryName))
            {

                return View();
            }
            if (checkCategory is not null)
            {
                ViewData["Error"] = "There is such a tag";
                return View();
            }
            Guid guid = Guid.NewGuid();
            Category newCategory = new()
            {
                Id = guid.ToString(),
                CategoryName = category.CategoryName,
                CreatedDate = DateTime.Now,
                UserId=CurrentUserId
                

            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string categoryId)
        {
            if (string.IsNullOrEmpty( categoryId  )) { return RedirectToAction("index"); }
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category is null) { return RedirectToAction("index"); };
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(string categoryId)
        {
            if (categoryId == null) return NotFound();
            Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id==categoryId );
            if (category == null) return NotFound();
           




            return View(category);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category )
        {
            try
            {
                var checkCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category.CategoryName);
                if (checkCategory != null)
                {
                    ViewBag.Error = "Tag name is already exists!";
                    return View();
                };
                Category updatedCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);


                updatedCategory.UpdateDate = DateTime.Now;
                updatedCategory.UpdatedUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                updatedCategory.CategoryName = category.CategoryName;
                var resultUpdate = _context.Categories.Update(updatedCategory);
                var result = await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
