using WebBanSach.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebBanSach.Models.Process
{
    public class OrderProcess
    {
        //khởi tạo dữ liệu từ tầng data
        BSDBContext db = null;

        //contructor
        public OrderProcess()
        {
            db = new BSDBContext();
        }


        public DonDatHang GetIdOrder(int id)
        {
            return db.DonDatHangs.Find(id);
        }


        public List<DonDatHang> ListOrder()
        {
            return db.DonDatHangs.OrderBy(x => x.MaDDH).ToList();
        }

        public int Insert(DonDatHang order)
        {
            db.DonDatHangs.Add(order);
            db.SaveChanges();
            return order.MaDDH;
        }
    }
}