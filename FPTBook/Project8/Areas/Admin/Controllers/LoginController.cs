using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models.Process;
using WebBanSach.Areas.Admin.Models;
using WebBanSach.Models.Data;

namespace WebBanSach.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        BSDBContext db = new BSDBContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminProfile()
        {
            return PartialView();
        }


        public ActionResult AdminInfo()
        {
            
            var model = Session["LoginAdmin"];

            
            if (ModelState.IsValid)
            {
                if (Session["LoginAdmin"] != null)
                {
                    
                    var result = db.Admins.SingleOrDefault(x => x.TaiKhoan == model);
                    
                    return View(result);
                }             
            }

            return View();
        }


        public ActionResult Logout()
        {
            
            Session["LoginAdmin"] = null;

            return Redirect("/Admin/Login");
        }


        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
          
            if (ModelState.IsValid)
            {
               
                var result = new AdminProcess().Login(model.TaiKhoan, model.MatKhau);
              
                if (result == 1)
                {
                   
                    Session["LoginAdmin"] = model.TaiKhoan;

                    return RedirectToAction("Index", "Home");                
                }
  
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account does not exist.");
                }
       
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Incorrect account or password, please check and collect again");
                }
            }

            return View();
        }
    }
}