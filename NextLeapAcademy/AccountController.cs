using Microsoft.AspNetCore.Mvc;
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
