using WebBanSach.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models.Process
{
    public class OderDetailProcess
    {
        BSDBContext db = null;
        public OderDetailProcess()
        {
            db = new BSDBContext();
        }


        public ChiTietDDH GetIdOrderDetail(int id)
        {
            return db.ChiTietDDHs.Find(id);
        }

        public List<ChiTietDDH> ListDetail(int id)
        {
            return db.ChiTietDDHs.Where(x => x.MaDDH == id).OrderBy(x => x.MaDDH).ToList();
        }


        public bool Insert(ChiTietDDH detail)
        {
            try
            {
                db.ChiTietDDHs.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}