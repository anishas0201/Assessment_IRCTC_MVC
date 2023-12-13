using Assessment_IRCTC_Revervation.DAL;
using Assessment_IRCTC_Revervation.Interface;
using Assessment_IRCTC_Revervation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.Controllers
{
    public class LoginController : Controller
    {
        RegisterInterface IRegister = new RegisterClass();
        public IActionResult SignUp()
        {
            return View();
        }
        public JsonResult SignUpAction(RegisterUser objmodel)
        {
            var res = IRegister.SignUpAction(objmodel);

            return Json(res);
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckLogin(RegisterUser objmodel)
        {
            var res = IRegister.CheckLogin(objmodel);
            if (res.status == true)
            {
                HttpContext.Session.SetString("text", objmodel.fullName);
                return RedirectToAction("Dashboard", "IRCTCMainBody");
            }
            TempData["error"] = "Invalid Credetials!!Please Check Again!!";
            return RedirectToAction("Login", "Login");
        }
    }
}
