
using BiznewWebUI.DTOs;
using BiznewWebUI.Helper;
using BiznewWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BiznewWebUI.Controllers
{
    public class AuthController : Controller
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        public readonly IEmailSender _emailSender;
        private readonly IOptions<TestSecret> _optionsSecret;
        private readonly IConfiguration _config;


        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> rolemanager = null, RoleManager<IdentityRole> roleManager = null, IEmailSender emailSender = null, IOptions<TestSecret> optionsSecret = null, IConfiguration config = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _optionsSecret = optionsSecret;
            _config = config;
        }

        public IActionResult Login()
        {
            var a = _config["ConnectionStrings:Movies"];
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
          
            return RedirectToAction(controllerName:"home",actionName:"index");
         
        }
        public IActionResult Register ()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterDTO user) 
        {
            try
            {

                if (!ModelState.IsValid) { ModelState.AddModelError("Error", "Data is Empty!"); return View(); }
                var checkEmail = await _userManager.FindByEmailAsync(user.Email);
                //if (checkEmail != null)
                //{
                //    ModelState.AddModelError("Error", "Email is already existed!");
                //    return View();
                //}

                User NewUser = new User()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.CreatingUserName(user.FirstName, user.LastName),
                           PhotoUrl = "/admin/img/undraw_profile.svg",
                    AboutAuthor = "/"
                };
                var result = await _userManager.CreateAsync(NewUser, user.Password);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(NewUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { token, email = user.Email }, Request.Scheme);

                MailHelper mailHelper = new MailHelper();
                bool successEmail = mailHelper.SendEmail(NewUser.Email, confirmationLink);
                if (result.Succeeded)
                    if (_userManager.Users.ToList().Count() == 1)
                    {
                        if (!_roleManager.Roles.Any(x=>x.Name=="Admin"))
                        {
                            IdentityRole newRole = new IdentityRole()
                            {
                                Name = "Admin"
                            };
                            await _roleManager.CreateAsync(newRole);
                            await _userManager.AddToRoleAsync(NewUser, "Admin");

                        }
                        else
                        {


                            await _userManager.AddToRoleAsync(NewUser, "Admin");
                        }
                    }
                    else
                    {
                        IdentityRole newRole = new IdentityRole()
                        {
                            Name = "User"
                        };
                        await _roleManager.CreateAsync(newRole);
                     
                        await _userManager.AddToRoleAsync(NewUser, "User");
                    }
               
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Error", item.Description);

                    }
                    return View();
                }

                return RedirectToAction(actionName: nameof(Login));
            }
            catch (Exception ex)
            {

             return View(ex.Message);
            }
          
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task< IActionResult> ConfirmEmail(string token, string email)
        {
            
            User user=_userManager.Users.FirstOrDefault(x => x.Email == email);
            if (user == null) {
                ViewData["CheckEmail"] = "Id yanlisdir";
                return RedirectToAction(nameof(Login));
            };

            var result = await _userManager.ConfirmEmailAsync(user, token);
          
            if (!result.Succeeded)
            {
                ViewData["CheckEmail"] = "Email Tesdiqlendi Indi Profilinize daxil Ola bilersiniz";
                return NotFound();
            }
            return RedirectToAction(nameof(Login));
        }
    }
}
