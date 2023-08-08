using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

                  CookieOptions options = new CookieOptions();
                  options.Expires = DateTime.Now.AddMinutes(5);
                  options.Secure = true;

                Response.Cookies.Append("myuserkey",userinputMaP.Username,options);


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
