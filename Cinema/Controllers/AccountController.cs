using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.Models;

namespace Cinema.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult LogIn()
        {
            var myLogin = new LogIn();

            return View("~/Views/LogIn.cshtml", myLogin);
        }

        [HttpPost]
        public ActionResult LogIn(LogIn loginResult)
        {
            if (ModelState.IsValid)
            {
                return View("~/Views/LogInResult.cshtml", loginResult);
            }
            return View("~/Views/LogIn.cshtml", loginResult);
        }
    }
}