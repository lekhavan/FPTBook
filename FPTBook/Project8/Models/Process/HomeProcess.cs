using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanSach.Models.Data;

namespace WebBanSach.Models.Process
{
    public class HomeProcess
    {

        BSDBContext db = null;


        public HomeProcess()
        {
            db = new BSDBContext();
        }


        public List<TheLoai> ListCategory()
        {
            return db.TheLoais.OrderBy(x => x.MaLoai).ToList();
        }


        public int InsertContact(LienHe entity)
        {
            db.LienHes.Add(entity);
            db.SaveChanges();

            return entity.MaLH;
        }

        public List<Sach> Search(string key)
        {
            return db.Saches.Where(x => x.TenSach.Contains(key)).OrderBy(x=>x.TenSach).ToList();
        }

    }
}