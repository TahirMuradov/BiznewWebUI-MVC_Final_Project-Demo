using BiznewWebUI.Data;
using BiznewWebUI.Models;
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


            var tags = _context.Tags
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
                Id = guid.ToString(),
                TagName = tag.TagName,
                CreatedDate = DateTime.Now,
                UserId = CurrentUserId,
               
                };
          await  _context.Tags.AddAsync(newTag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");                   
        }
      
        public async Task<IActionResult> Delete(string tagId)
        {
            if (string.IsNullOrEmpty(tagId)) { return RedirectToAction("index"); }
              var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if(tag is null) { return RedirectToAction("index"); };
             _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task <IActionResult> Edit(string tagId)
        {
            if (tagId == null) return NotFound();
          Tag? tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
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

