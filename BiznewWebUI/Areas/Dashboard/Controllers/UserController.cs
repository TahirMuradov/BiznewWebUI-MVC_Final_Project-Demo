﻿using BiznewWebUI.Areas.Dashboard.ViewModels;
using BiznewWebUI.Data;
using BiznewWebUI.DTOs;
using BiznewWebUI.Helper;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using System.Text.Json;

namespace BiznewWebUI.Areas.Dashboard.Controllers
{
    [Area(nameof(Dashboard))]

    public class UserController : Controller
    {
        public readonly UserManager<User> _userManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly AppDbContext _context;
        public readonly IHttpContextAccessor _contextAccessor;
        public readonly IWebHostEnvironment _env;
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor, IWebHostEnvironment env, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _env = env;
            _context = context;
        }


        public async Task< IActionResult> Index()
        {

            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var users = _userManager.Users.ToList();
            var checkedUser=users.FirstOrDefault(users => users.Id == userId);
                                 
            var checkedUserRoles = await _userManager.GetRolesAsync(checkedUser);
            //var resultRole = checkedUserRoles.Contains("Admin");
            //if (!resultRole)
            //{
            //    TempData["ErrorRole"] = "Sizin Selahiyetiniz bu xidmetden istifade etmek ucun yeterli deyil";
            //    return RedirectToAction(controllerName: "Home", actionName: "Index");
            //}
            return View(users);

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (userId==null)          
                return NotFound();
                
            User user=await _userManager.FindByIdAsync(userId);
         
            if (user == null)
                return NotFound();
            bool chechFoto = _context.Users.Any(x => x.PhotoUrl == user.PhotoUrl && x.Id != user.Id);
            if (!chechFoto)
            {
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(_env.WebRootPath, "uploads").ToLower());
           //directoryInfo.GetFiles().Where(x => x.FullName == Path.Combine(_env.WebRootPath, user.PhotoUrl));
                for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
                {
                    if (directoryInfo.GetFiles()[i].FullName == Path.Combine(_env.WebRootPath, user.PhotoUrl))
                    {
                        directoryInfo.GetFiles()[i].Delete();
                    }
                }

            }
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return NotFound();
            return RedirectToAction("Index");
        }
       
        public async Task< IActionResult> AddRole(string userId)
        {
            if (userId == null) { return NotFound(); }
            var checkUser= await _userManager.FindByIdAsync(userId);
            if (checkUser== null) return NotFound();
            var userRoles= (await _userManager.GetRolesAsync(checkUser)).ToList();
            var roles=_roleManager.Roles.Select(x=>x.Name).ToList();
            UserRoleVM userRoleVM = new UserRoleVM()
            {
                User = checkUser,
                Roles = roles.Except(userRoles),
            };
            return View(userRoleVM);
                 
            
           
        }

        [HttpPost]
        
        public async Task< IActionResult> AddRole(string userId,string role) {

            if (userId == null) { return View(); }
            User checkUser = await _userManager.FindByIdAsync(userId);
            if (checkUser == null) { return NotFound(); }
            var userAddRole = await _userManager.AddToRoleAsync(checkUser, role);
            if (!userAddRole.Succeeded)
            {
                ViewData["Error"] = "Something went wrong!";
                return View();
            }
            return RedirectToAction(actionName: "index");   

        }
        public IActionResult Settings()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var users = _userManager.Users.ToList();
            User checkedUser = users.FirstOrDefault(users => users.Id == userId);
            UserEditDTO registerDTO = new UserEditDTO()
            {
                
                FirstName = checkedUser.FirstName,
                LastName = checkedUser.LastName,
                UserName=checkedUser.UserName,
                
                
                
                
            };
            return View(registerDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Settings(UserEditDTO editUser,IFormFile photo)
        {

            if (editUser.FirstName==null||editUser.LastName==null||editUser.UserName==null)
            {
              
                return View();
            }
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var users = _userManager.Users.ToList();
            User checkedUser = users.FirstOrDefault(users => users.Id == userId);

            checkedUser.FirstName = editUser.FirstName;
            checkedUser.LastName = editUser.LastName;
            checkedUser.UserName = editUser.UserName;
            
            if (photo is not null)
            {

            string PhotoUrl = await photo.SaveFileAsync(_env.WebRootPath);
                if (checkedUser.PhotoUrl != "~/admin/img/undraw_profile.svg")
                {
                    FileInfo photoFile = new FileInfo(_env.WebRootPath + checkedUser.PhotoUrl);
                    photoFile.Delete();
                }
                checkedUser.PhotoUrl = PhotoUrl;

            }
            




            if (editUser.NewPassword is not null)
            {
            IdentityResult result = await _userManager.ChangePasswordAsync(checkedUser, editUser.CurrentPassword,editUser.NewPassword);
                
            }
            var updateResult=  await _userManager.UpdateAsync(checkedUser);

            return View();
        }
     


    }
}
