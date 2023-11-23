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
 
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public TagController(AppDbContext context, UserManager<User> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {


            var tags = _context.Tags.Where(x=>x.IsDeleted==false)
                .Include(x => x.User)
                .ToList();



            return View(tags);
           
        }
        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(Tag tag)
        {
            
          
            var checkTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == tag.TagName);
            var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(tag.TagName))
            {
              
                return View();
            }
            if (checkTag is not null)
            {
                ViewData["Error"] = "There is such a tag";
                return View();
            }
            Guid guid = Guid.NewGuid();
            Tag newTag = new Tag()
            {
                
                TagName = tag.TagName,
                CreatedDate = DateTime.Now,
                UserId = CurrentUserId,
               
                };
          await  _context.Tags.AddAsync(newTag);
            User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == CurrentUserId);
            UserActions newAction = new UserActions
            {
                ActionName = "Create",
                Tag = newTag,
                
                DateTime = DateTime.Now,
               
                userId = CurrentUserId,
                IsActive = true

            };
            await _context.UserActions.AddAsync(newAction);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");                   
        }
      
        public async Task<IActionResult> Delete(string tagId)
        {
            if (string.IsNullOrEmpty(tagId)) { return RedirectToAction("index"); }
              var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id.ToString() == tagId);
            if(tag is null) { return RedirectToAction("index"); };
            tag.IsDeleted = true;
             _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task <IActionResult> Edit(string tagId)
        {
            if (tagId == null) return NotFound();
          Tag? tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id.ToString() == tagId);
            tag.UpdateDate= DateTime.Now;
           
            if (tag == null) return NotFound();
          
           

            return View(tag);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Tag tag)
        {
            try
            {
                var checkTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == tag.TagName);
                if (checkTag != null)
                {
                    ViewBag.Error = "Tag name is already exists!";
                    return View();
                };
                Tag updatedTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

             
                updatedTag.UpdateDate = DateTime.Now;
                updatedTag.UpdatedUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
               updatedTag.TagName = tag.TagName;
            var resultUpdate= _context.Tags.Update(updatedTag);
                var CurrentUserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                User currentUser = _userManager.Users.FirstOrDefault(x => x.Id == CurrentUserId);
                Models.UserActions newAction = new Models.UserActions
                {
                    ActionName = "Edit",
                    Tag = updatedTag,
                    DateTime = DateTime.Now,
               
                    userId = CurrentUserId,
                    IsActive = true

                };
                await _context.UserActions.AddAsync(newAction);
                var result= await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
    }

