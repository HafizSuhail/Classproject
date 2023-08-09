using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NextLeapAcademy.BusinessEntities;
using NextLeapAcademy.Models;

namespace NextLeapAcademy
{
    public class AccountController : Controller
    {
        public IActionResult Login()

        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitLogin(LoginModel userinputMaP)
        {
            if (ModelState.IsValid)
            {
                //we will send request to DB to check the user name & password
                //if we have user with user name and password, then user will be redirected to home page
                //else we will show validation message

                var bdcontext = new Nextleapdbcontex();

                User UserEntity = bdcontext.Users
                    .FirstOrDefault(p => p.Email == userinputMaP.Username && p.Password == userinputMaP.Password);
                if (UserEntity is null) 
                {
                    // there is no user with email and password provided
                    ModelState.AddModelError("", "Login Failed, please validate your username & password!");

                    return View("Login", userinputMaP);

                }

                HttpContext.Session.SetString("Username", UserEntity.UserName);






                //CookieOptions options = new CookieOptions();
                //  options.Expires = DateTime.Now.AddMinutes(5);
                //  options.Secure = true;

                //Response.Cookies.Append("myuserkey",userinputMaP.Username,options);


                return RedirectToAction("Home", "Home");
            }
            else 
            {
                ModelState.AddModelError("", "Login Failed, please validate your username & password!");
                return View("Login", userinputMaP); 
            }
        
        }
    }
}
