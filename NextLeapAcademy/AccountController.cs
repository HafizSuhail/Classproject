using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NextLeapAcademy.BusinessEntities;
using NextLeapAcademy.Models;
using System.Security.Claims;

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

                //User is valid and successful login

                string userId = UserEntity.UserId.ToString();
                string userName = UserEntity.UserName;
                string userEmail = UserEntity.Email;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.UserData, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, userEmail)
                };

                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = false,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    IssuedUtc = DateTimeOffset.Now,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, authProperties);



                //HttpContext.Session.SetString("Username", UserEntity.UserName);

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


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }



    }


}
