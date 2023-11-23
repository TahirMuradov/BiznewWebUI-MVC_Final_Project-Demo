using BiznewWebUI.Data;
using BiznewWebUI.Helper;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
  
    public class AdvortController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdvortController(AppDbContext context, UserManager<User> userManager, IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _env = env;
        }

        public IActionResult Index()
        {
            var advorts= _context.Advorts.Where(x => x.IsDeleted == false)
                .Include(q=>q.AdvortsArticles)
                .ThenInclude(w=>w.Articles)
                .Include(e=>e.User)

                .ToList();
            return View(advorts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(Advort advort ,IFormFile Photo)
        {
            string currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            
            advort.CreatedDate = DateTime.Now;
            advort.UserId = currentUserId;
          
            advort.Image = await FileHelper.SaveFileAsync(Photo, _env.WebRootPath);
            await _context.Advorts.AddAsync(advort);

            Models.UserActions newAction = new Models.UserActions
            {ActionName="Create",
            Advort=advort,
            DateTime=DateTime.Now,
       
            userId=currentUserId,
            IsActive=true

            };
            await _context.UserActions.AddAsync(newAction);
                 
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async  Task<IActionResult> Edit(string advortId)
        {
            Advort advort = await _context.Advorts.FirstOrDefaultAsync(x => x.Id.ToString()== advortId);

            return View(advort);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Advort advort,IFormFile Photo)
        {
            Advort updatedAdvort = await _context.Advorts.FirstOrDefaultAsync(x => x.Id == advort.Id);
            string currentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User currentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
            if (currentUserId==null)
            {
                return NotFound();
            }
           updatedAdvort.UpdateDate = DateTime.Now;
        updatedAdvort.UpdatedUserId = currentUserId;
            updatedAdvort.Link = advort.Link;
            updatedAdvort.AdvortName = advort.AdvortName;
         
           if(Photo is not null)
            {

            updatedAdvort.Image = await FileHelper.SaveFileAsync(Photo, _env.WebRootPath);
            }
            Models.UserActions newAction = new Models.UserActions
            {
                ActionName = "Edit",
                Advort = advort,
                DateTime = DateTime.Now,
               
                userId = currentUserId,
                IsActive = true

            };
            await _context.UserActions.AddAsync(newAction);

            _context.Advorts.Update(updatedAdvort);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public async Task< IActionResult> Delete (Guid advortId)
        {
            if (advortId==null) RedirectToAction("index");
            Advort advort = await _context.Advorts.FirstOrDefaultAsync(x=>x.Id == advortId);
            advort.IsDeleted = true;
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == CurrentUserId);
            Models.UserActions newAction = new Models.UserActions
            {
                ActionName = "Delete",
                Advort = advort,
                DateTime = DateTime.Now,
               
                userId = CurrentUserId,
                IsActive = true

            };
            await _context.UserActions.AddAsync(newAction);
            _context.Advorts.Update(advort);
           await  _context.SaveChangesAsync();
                return RedirectToAction("Index");
        }
    }
}
