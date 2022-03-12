using WebBanSach.Models.Data;
using WebBanSach.Models.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WebBanSach.Controllers
{
    public class HomeController : Controller
    {
     
        BSDBContext db = new BSDBContext();

   
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult ShowCategory()
        {
        
            var result = new HomeProcess().ListCategory();

            return PartialView(result);
        }

        public ActionResult ThemesBook(int id)
        {
            var tenloai = new AdminProcess().GetIdCategory(id);
            ViewBag.TenLoai = tenloai.TenLoai;

            var result = new BookProcess().ThemeBook(id);
            return View(result);
        }

   
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

      
        public ActionResult QuyDinh()
        {
            return View();
        }

        public ActionResult Thanhtoan()
        {
            return View();
        }
        public ActionResult VanChuyen()
        {
            return View();
        }
        public ActionResult DoiTra()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }

   
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "We are happy to answer your questions!";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(LienHe model)
        {
            if (ModelState.IsValid)
            {
                var home = new HomeProcess();
                var lh = new LienHe();

  
                lh.Ten = model.Ten;
                lh.Ho = model.Ho;
                lh.Email = model.Email;
                lh.DienThoai = model.DienThoai;
                lh.NoiDung = model.NoiDung;
                lh.NgayCapNhat = DateTime.Now;


                var result = home.InsertContact(lh);

                if (result > 0)
                {
                    ViewBag.success = "Your feedback has been received";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Recording error");
                }
            }

            return View(model);
        }

   
        [HttpGet]
        public ActionResult SearchResult(int? page, string key)
        {
            ViewBag.Key = key;

           
            int pageNumber = (page ?? 1);
            int pageSize = 6;

            var result = new HomeProcess().Search(key).ToPagedList(pageNumber, pageSize);

            if (result.Count == 0 || key==null || key=="")
            {
                ViewBag.ThongBao = "No product found";
                return View(result);
            }
            ViewBag.ThongBao = "There are " + result.Count + " results on this page";

            return View(result);
        }

        
        [HttpPost]
        public ActionResult SearchResult(int? page, FormCollection f)
        {
           
            string key = f["txtSearch"].ToString();

            ViewBag.Key = key;

           
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            
            var result = new HomeProcess().Search(key).ToPagedList(pageNumber, pageSize);

            if (result.Count == 0 || key == null || key == "")
            {
                ViewBag.ThongBao = "No product found";
                return View(result);
            }
            ViewBag.ThongBao = "There are " + result.Count + " results on this page";

            return View(result);
        }

    }
}