using foa.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Dao;
using System.Web.Mvc;
using foa.Common;

namespace foa.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            var dao = new UserDao();
            if(string.IsNullOrEmpty(model.UserName))
            {
                ModelState.AddModelError("", "Username invalid");
            } else if(string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Password invalid");
            } else
            {
                var result = dao.Login(model.UserName, model.Password);
                if (result)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.username;
                    userSession.UserId = user.id;
                    Session.Add(Constant.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login invalid");
                }
            }
            return View("Index");
            
        }
    }
}