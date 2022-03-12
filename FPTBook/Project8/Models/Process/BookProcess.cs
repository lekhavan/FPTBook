using WebBanSach.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models.Process
{
    public class BookProcess
    {

        BSDBContext db = null;


        public BookProcess()
        {
            db = new BSDBContext();
        }

  
        public List<Sach> NewDateBook(int count)
        {
            return db.Saches.OrderByDescending(x => x.NgayCapNhat).Take(count).ToList();
        }

 
        public List<Sach> ThemeBook(int id)
        {
            return db.Saches.Where(x => x.MaLoai == id).ToList();
        }


        public List<Sach> TakeBook(int count)
        {
            return db.Saches.OrderBy(x => x.NgayCapNhat).Take(count).ToList();
        }

   
        public List<Sach> ShowAllBook()
        {
            return db.Saches.OrderBy(x => x.MaSach).ToList();
        }

    }
}