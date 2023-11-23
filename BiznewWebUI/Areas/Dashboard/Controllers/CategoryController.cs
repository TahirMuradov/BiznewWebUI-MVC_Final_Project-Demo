using BiznewWebUI.Data;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Authorization;
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
            var category=_context.Categories.Where(x=>x.IsDeleted==false)
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
            User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == CurrentUserId);
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
               
                CategoryName = category.CategoryName,
                CreatedDate = DateTime.Now,
                UserId=CurrentUserId
                

            };
            await _context.Categories.AddAsync(newCategory);
            Models.UserActions newAction = new Models.UserActions
            {
                ActionName = "Create",
                Category = newCategory,
                DateTime = DateTime.Now,
            
                userId = CurrentUserId,
                IsActive = true

            };
            await _context.UserActions.AddAsync(newAction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string categoryId)
        {
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == CurrentUserId);
            if (string.IsNullOrEmpty( categoryId  )) { return RedirectToAction("index"); }
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == categoryId);
            if (category is null) { return RedirectToAction("index"); };
            category.IsDeleted = true;
            _context.Categories.Update(category);
            Models.UserActions newAction = new Models.UserActions
            {
                ActionName = "Delete",
                Category = category,
                DateTime = DateTime.Now,
           
                userId = CurrentUserId,
                IsActive = true

            };
            await _context.UserActions.AddAsync(newAction);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(string categoryId)
        {
            if (categoryId == null) return NotFound();
            Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id.ToString() ==categoryId );
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
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == CurrentUserId);
                Models.UserActions newAction = new Models.UserActions
                {
                    ActionName = "Edit",
                    Category = updatedCategory,
                    
                    DateTime = DateTime.Now,
              
                    userId = CurrentUserId,
                    IsActive = true

                };
                await _context.UserActions.AddAsync(newAction);
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
