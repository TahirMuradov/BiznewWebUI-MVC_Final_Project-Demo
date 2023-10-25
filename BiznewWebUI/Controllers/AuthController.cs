
using BiznewWebUI.DTOs;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BiznewWebUI.Controllers
{
    public class AuthController : Controller
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Password or Email is empty!");
                return View();
            }
            var checkEmail= await _userManager.FindByEmailAsync(user.Email);

            if (checkEmail==null)
            {

                ModelState.AddModelError("Error", "Email or Password is not valid!");
                return View();
                }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(checkEmail, user.Password, user.RememverMe, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("Error", "Email or Password is not valid!");
                return View();
            }
            return RedirectToAction("Index", "Home");
         
        }
        public IActionResult Register ()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterDTO user) {
            if(!ModelState.IsValid) { ModelState.AddModelError("Error", "Data is Empty!"); return View(); }
            var checkEmail = await _userManager.FindByEmailAsync(user.Email);
            if(checkEmail!= null)
            {
                 ModelState.AddModelError("Error", "Email is already existed!");
                return View(); 
            }
            User NewUser = new User()
            { Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName=user.FirstName+user.LastName,
            
            

            };

            await _userManager.CreateAsync(NewUser,user.Password);
            

            return RedirectToAction(actionName:nameof(Login));
        }
    }
}
