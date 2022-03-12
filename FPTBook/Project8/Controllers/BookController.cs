using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models.Data;
using WebBanSach.Models.Process;
using PagedList;
using PagedList.Mvc;

namespace WebBanSach.Controllers
{
    public class BookController : Controller
    {
        BSDBContext db = new BSDBContext();
        // GET: Book
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopDateBook()
        {
            var result = new BookProcess().NewDateBook(6);
            return PartialView(result);
        }


        public ActionResult Details(int id)
        {
            var result = new AdminProcess().GetIdBook(id);

            return View(result);
        }


        public ActionResult FavoriteBook()
        {
            var result = new BookProcess().NewDateBook(3);

            return PartialView(result);
        }


        public ActionResult DidYouSee()
        {
            var result = new BookProcess().TakeBook(3);

            return PartialView(result);
        }


        public ActionResult ShowAllBook(int? page)
        {

            int pageSize = 10;


            int pageNumber = (page ?? 1);

            var result = new BookProcess().ShowAllBook().ToPagedList(pageNumber, pageSize);

            return View(result);
        }

    }
}