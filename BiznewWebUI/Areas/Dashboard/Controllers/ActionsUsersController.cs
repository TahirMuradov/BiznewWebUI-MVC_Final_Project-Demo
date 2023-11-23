using BiznewWebUI.Data;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]
    [Authorize]
    public class ActionsUsersController : Controller
    {
        private readonly AppDbContext _context;

        public ActionsUsersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var alertActions = _context.UserActions
            
             
           
            .ToList();
           

            return View(alertActions);
        }
        [HttpPost]
        public async Task<IActionResult> index(string actionId)
        {
            if (string.IsNullOrEmpty(actionId))
                return NotFound();
            UserActions action = await _context.UserActions.FirstOrDefaultAsync(x => x.Id.ToString() == actionId);
            if (action == null)return NotFound();
        
            action.IsActive = false;
            _context.Update(action);
            await _context.SaveChangesAsync();
            return View();
        }
        public async Task<JsonResult> GetPartialViewData()
        {
      
           
            var actions = _context.UserActions.Where(x=>x.IsActive==true).ToList();
                
                
            List<UserActions> actionsList = new List<UserActions>();
            if (actions.Count<3)
            {
                
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].IsActive = false;
                actionsList.Add(actions[i]);
            }
            }
            else
            {
                for (int i = 0; i <=3; i++)
                {
                    actions[i].IsActive = false;
                    actionsList.Add(actions[i]);
                }
            }
           
            _context.UpdateRange(actionsList);
            int saveResult = await _context.SaveChangesAsync();

          
            return Json(saveResult);
        }
    }
}
