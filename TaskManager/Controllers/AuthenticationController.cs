using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login(string returnUrl)
        {
            var model = new Login
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var profileRepo = new ProfileRepository();
            var profile = profileRepo.GetByEmailPassword(model.Email, model.Password);

            if (profile != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, profile.Name),
                    new Claim(ClaimTypes.Email, profile.Email),
                    new Claim(ClaimTypes.Role, profile.Role.Description)
                }, "ApplicationCookie");
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;

                authManager.SignIn(identity);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            ModelState.AddModelError("", "Usuário ou senha inválido");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "task");
            }
            return returnUrl;
        }


    }

}