using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models.Data;
using WebBanSach.Models.Process;
using System.IO;
using Project8.Areas.Admin.Code;

namespace WebBanSach.Areas.Admin.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        //Trang quản lý

        BSDBContext db = new BSDBContext();

        public ActionResult Index()
        {
            return View();
        }

        #region Sản phẩm

        [HttpGet]
        public ActionResult ShowListBook()
        {
            var model = new AdminProcess().ListAllBook();

            return View(model);
        }

        public ActionResult AddBook()
        {
            ViewBag.MaLoai = new SelectList(db.TheLoais.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG");

            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Sach sach, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaLoai = new SelectList(db.TheLoais.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG");

            if (fileUpload == null)
            {
                ViewBag.Alert = "Please choose cover photo";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("/images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Alert = "Image already exists";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    sach.AnhBia = fileName;
                    var result = new AdminProcess().InsertBook(sach);
                    if (result > 0)
                    {
                        ViewBag.Success = "Successfully added";

                        ModelState.Clear();
                    }
                    else
                    {
                        ModelState.AddModelError("", "add failed.");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult DetailsBook(int id)
        {

            var sach = new AdminProcess().GetIdBook(id);

            return View(sach);
        }

        public ActionResult UpdateBook(int id)
        {
    
            var sach = new AdminProcess().GetIdBook(id);

            ViewBag.MaLoai = new SelectList(db.TheLoais.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG", sach.MaTG);

            return View(sach);
        }

        [HttpPost]
        public ActionResult UpdateBook(Sach sach, HttpPostedFileBase fileUpload)
        {
            
            ViewBag.MaLoai = new SelectList(db.TheLoais.ToList().OrderBy(x => x.TenLoai), "MaLoai", "TenLoai", sach.MaLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(x => x.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaTG = new SelectList(db.TacGias.ToList().OrderBy(x => x.TenTG), "MaTG", "TenTG", sach.MaTG);

            
            if (fileUpload == null)
            {

                if (ModelState.IsValid)
                {

                    var result = new AdminProcess().UpdateBook(sach);

                    if (result == 1)
                    {
                        ViewBag.Success = "Update successful";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update failed.");
                    }
                }
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("/images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Alert = "Image already exists";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                    sach.AnhBia = fileName;
                    var result = new AdminProcess().UpdateBook(sach);
                    if (result == 1)
                    {
                        ViewBag.Success = "Update successful";
                    }
                    else
                    {
                        ModelState.AddModelError("", "update failed.");
                    }
                }
            }

            return View(sach);
        }


        [HttpDelete]
        public ActionResult DeleteBook(int id)
        {

            new AdminProcess().DeleteBook(id);


            return RedirectToAction("ShowListBook");
        }


        [HttpGet]
        public ActionResult ShowListCategory()
        {

            var model = new AdminProcess().ListAllCategory();

            return View(model);
        }


        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCategory(TheLoai model)
        {
            
            if (ModelState.IsValid)
            {
                
                var admin = new AdminProcess();

              
                var tl = new TheLoai();

                
                tl.TenLoai = model.TenLoai;

                
                var result = admin.InsertCategory(tl);

                
                if (result > 0)
                {
                    ViewBag.Success = "Successfully added";
                    
                    ModelState.Clear();

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Add failed.");
                }
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {

            var tl = new AdminProcess().GetIdCategory(id);


            return View(tl);
        }


        [HttpPost]
        public ActionResult UpdateCategory(TheLoai tl)
        {
          
            if (ModelState.IsValid)
            {
            
                var admin = new AdminProcess();

           
                var result = admin.UpdateCategory(tl);

          
                if (result == 1)
                {
                    return RedirectToAction("ShowListCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Update failed.");
                }
            }

            return View(tl);
        }

       
        [HttpDelete]
        public ActionResult DeleteCategory(int id)
        {
            
            new AdminProcess().DeleteCategory(id);

         
            return RedirectToAction("ShowListCategory");
        }

     
        [HttpGet]
        public ActionResult ShowListAuthor()
        {
        
            var model = new AdminProcess().ListAllAuthor();

     
            return View(model);
        }

     
        public ActionResult AddAuthor()
        {
            return View();
        }

    
        [HttpPost]
        public ActionResult AddAuthor(TacGia model)
        {
           
            if (ModelState.IsValid)
            {
              
                var admin = new AdminProcess();

                var tg = new TacGia();

           
                tg.TenTG = model.TenTG;
                tg.QueQuan = model.QueQuan;
                tg.NgaySinh = model.NgaySinh;
                tg.NgayMat = model.NgayMat;
                tg.TieuSu = model.TieuSu;

         
                var result = admin.InsertAuthor(tg);

         
                if (result > 0)
                {
                    ViewBag.Success = "Successfully added";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Add failed.");
                }
            }

            return View(model);
        }

       
        [HttpGet]
        public ActionResult UpdateAuthor(int id)
        {
           
            var tg = new AdminProcess().GetIdAuthor(id);

            return View(tg);
        }

      
        [HttpPost]
        public ActionResult UpdateAuthor(TacGia tg)
        {
           
            if (ModelState.IsValid)
            {
              
                var admin = new AdminProcess();

                
                var result = admin.UpdateAuthor(tg);
                
                if (result == 1)
                {
                    ViewBag.Success = "Update successful";
                }
                else
                {
                    ModelState.AddModelError("", "Update failed.");
                }
            }

            return View(tg);
        }

       
        [HttpDelete]
        public ActionResult DeleteAuthor(int id)
        {
          
            new AdminProcess().DeleteAuthor(id);

            return RedirectToAction("ShowListAuthor");
        }

       
        [HttpGet]
        public ActionResult ShowListPublish()
        {
           
            var model = new AdminProcess().ListAllPublish();

            return View(model);
        }

        
        public ActionResult AddPublish()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult AddPublish(NhaXuatBan model)
        {
            
            if (ModelState.IsValid)
            {
                
                var admin = new AdminProcess();

               
                var nxb = new NhaXuatBan();

               
                nxb.TenNXB = model.TenNXB;
                nxb.DiaChi = model.DiaChi;
                nxb.DienThoai = model.DienThoai;

               
                var result = admin.InsertPublish(nxb);
               
                if (result > 0)
                {
                    ViewBag.Success = "Successfully added";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Add failed.");
                }
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult UpdatePublish(int id)
        {
            var nxb = new AdminProcess().GetIdPublish(id);

            return View(nxb);
        }
        [HttpPost]
        public ActionResult UpdatePublish(NhaXuatBan nxb)
        {
            if (ModelState.IsValid)
            {
                var admin = new AdminProcess();

                var result = admin.UpdatePublish(nxb);
                if (result == 1)
                {
                    ViewBag.Success = "Update successful";
                }
                else
                {
                    ModelState.AddModelError("", "Update failed.");
                }
            }

            return View(nxb);
        }

 
        [HttpDelete]
        public ActionResult DeletePublish(int id)
        {

            new AdminProcess().DeletePublish(id);


            return RedirectToAction("ShowListPublish");
        }

        #endregion

        #region Phản hồi

     

        [HttpGet]
     
        public ActionResult FeedBack()
        {
            var result = new AdminProcess().ShowListContact();

            return View(result);
        }

       
        public ActionResult FeedDetail(int id)
        {
            var result = new AdminProcess().GetIdContact(id);

            return View(result);
        }

   
        [HttpDelete]
        public ActionResult DeleteFeedBack(int id)
        {
            new AdminProcess().deleteContact(id);

            return RedirectToAction("FeedBack");
        }

        #endregion

        #region Người dùng

       
        public ActionResult ShowUser()
        {
            var result = new AdminProcess().ListUser();

            return View(result);
        }

     
        public ActionResult DetailsUser(int id)
        {
            var result = new AdminProcess().GetIdCustomer(id);

            return View(result);
        }

      
        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            new AdminProcess().DeleteUser(id);

            return RedirectToAction("ShowUser");
        }

        #endregion

        #region Đơn đặt hàng

        
        public ActionResult Order()
        {
            var result = new OrderProcess().ListOrder();

            return View(result);
        }

     
        public ActionResult DetailsOrder(int id)
        {
            var result = new OderDetailProcess().ListDetail(id);

            return View(result);
        }

        #endregion

    }
}