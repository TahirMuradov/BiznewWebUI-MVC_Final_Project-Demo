using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using Microsoft.AspNetCore.Mvc;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    public class RolesController : Controller
    {
        public readonly RoleManager<IdentityRole> _rolemanager;

        public RolesController(RoleManager<IdentityRole> rolemanager)
        {
            _rolemanager = rolemanager;
        }

        public IActionResult Index()
        {
            var roles=_rolemanager.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(IdentityRole role)
        {
            if(role.Name==null)
            {
                ViewData["Error"] = "Role Name Is empty!";
                return View();
            }
            await _rolemanager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task< IActionResult> Delete(string roleId)
        {
           IdentityRole role = _rolemanager.Roles.FirstOrDefault(r => r.Id == roleId);
            if(roleId==null) {
                ModelState.AddModelError("Error", "Role Id or Name is Not found!");
                return View();
            }
           IdentityResult result= await _rolemanager.DeleteAsync(role);
            if (!result.Succeeded)
            {
               ModelState.AddModelError("Error", "Something went wrong!");
                return View();
            }

            return RedirectToAction(actionName:"index");
        }
        public async Task< IActionResult> Edit (string roleId)
        {
            if (roleId == null)
            {
                ModelState.AddModelError("Error", "Role Id or Name is Not found!");
                return View();
            }
           IdentityRole Role = await _rolemanager.FindByIdAsync(roleId);
            
            if (Role == null)
            {
                ModelState.AddModelError("Error", "Role Id or Name is Not found!");
                return View();
            }
           
            return View(Role);
        }
       
    }
}
