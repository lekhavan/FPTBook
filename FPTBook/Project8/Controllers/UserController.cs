using WebBanSach.Areas.Admin.Models;
using WebBanSach.Models.Data;
using WebBanSach.Models.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace WebBanSach.Controllers
{
    public class UserController : Controller
    {
       
        BSDBContext db = new BSDBContext();
        public static KhachHang khachhangstatic;
        [HttpGet]
      
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Register(KhachHang model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserProcess();

                var kh = new KhachHang();

                if (user.CheckUsername(model.TaiKhoan, model.MatKhau) == 1)
                {
                    ModelState.AddModelError("", "Account already exists");
                }
                else if (user.CheckUsername(model.TaiKhoan, model.MatKhau) == -1)
                {
                    ModelState.AddModelError("", "Account already exists");
                }
                else
                {
                    kh.TaiKhoan = model.TaiKhoan;
                    kh.MatKhau = model.MatKhau;
                    kh.TenKH = model.TenKH;
                    kh.Email = model.Email;
                    kh.DiaChi = model.DiaChi;
                    kh.DienThoai = model.DienThoai;
                    kh.NgaySinh = model.NgaySinh;
                    kh.NgayTao = DateTime.Now;
                    kh.TrangThai = false;
                    
                    var result = user.InsertUser(kh);
                    
                    var idUser = db.KhachHangs.FirstOrDefault(n => n.Email == kh.Email && n.TenKH == kh.TenKH);
                    BuildUserTemplate(idUser.MaKH);
                    if (result > 0)
                    {
                        
                        ModelState.Clear();
                        return RedirectToAction("CheckActivationNotification", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed.");
                    }
                }
                            
            }

            return View(model);
        }

        public ActionResult XacNhan(int khMaKh)
        {
            ViewBag.Makh = khMaKh;
            return View();
        }

        public JsonResult XacNhanEmail(int khMaKh)
        {
            KhachHang Data = db.KhachHangs.Where(x => x.MaKH == khMaKh).FirstOrDefault();
            Data.TrangThai = true;
            db.SaveChanges();
            var msg = "Email Confirmed!";
            Session["User"] = null;
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildUserTemplate(int khMaKh)
        {
            string body =
                System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var inforKH = db.KhachHangs.Where(x => x.MaKH == khMaKh).First();
            var url = "https://webbansach17dtha3.cf/" + "User/XacNhan?khMaKh="+khMaKh;
            body = body.Replace("@ViewBag.LinkXacNhan", url);
            body = body.ToString();
            BuildEmailTemplate("Account Created Successfully", body, inforKH.Email);

        }

        public void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "webbansach17dtha3@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, new ContentType("text/html")));
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("vanlkgcc19135@fpt.edu.vn","fptbookstore");
            /*try
            {
                client.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }*/
        }

        public ActionResult ActivationNotice()
        {
            return View();
        }
        public ActionResult checkactivationnotification()
        {
            return View();
        }


        public ActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginPage(LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                
                var result = new UserProcess().Login(model.TaiKhoan, model.MatKhau);
               
                if (result == 1)
                {
                    
                    Session["User"] = model.TaiKhoan;
                    var kh = db.KhachHangs.Where(x => x.TaiKhoan == model.TaiKhoan).FirstOrDefault();
                    khachhangstatic = kh;
                   
                    return RedirectToAction("Index", "Home");
                }
                
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account does not exist.");
                    
                }
               
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Incorrect account or password");
                    
                }
            }

            return View();
        }

 
        
        [ChildActionOnly]
        public ActionResult Login()
        {
            return PartialView();
        }

       
        [HttpPost]
        [ChildActionOnly]
        public ActionResult Login(LoginModel model)
        {
            
            if (ModelState.IsValid)
            {
                
                var result = new UserProcess().Login(model.TaiKhoan, model.MatKhau);

               
                if (result == 1)
                {
                    
                    Session["User"] = model.TaiKhoan;
                    var kh = db.KhachHangs.Where(x => x.TaiKhoan == model.TaiKhoan).FirstOrDefault();
                    khachhangstatic = kh;
                  
                    return RedirectToAction("Index", "Home");
                }
                
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account does not exist.");
                  
                }
             
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Incorrect account or password");
                  
                }
            }

            return PartialView();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["User"] = null;
            khachhangstatic = null;
            return RedirectToAction("Index", "Home");
        }

       
        [HttpGet]
        public ActionResult EditUser()
        {
            
            var model = Session["User"];

            if (ModelState.IsValid)
            {
                
                var result = db.KhachHangs.SingleOrDefault(x => x.TaiKhoan == model);

                
                return View(result);
            }

            return View();
        }

       
        public ActionResult EditUser(KhachHang model)
        {
            if (ModelState.IsValid)
            {
               
                var result = new UserProcess().UpdateUser(model);

                
                if (result == 1)
                {
                    return RedirectToAction("EditUser");                  
                }
                else
                {
                    ModelState.AddModelError("", "Update failed.");
                }
            }

            return View(model);
        }

    }
}