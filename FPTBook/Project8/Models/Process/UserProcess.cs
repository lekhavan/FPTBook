using WebBanSach.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanSach.Models.Process;

namespace WebBanSach.Models.Process
{
    public class UserProcess
    {

        BSDBContext db = null;

        public UserProcess()
        {
            db = new BSDBContext();
        }

        public KhachHang GetIdUser(int id)
        {
            return db.KhachHangs.Find(id);
        }

        public int InsertUser(KhachHang entity)
        {
            db.KhachHangs.Add(entity);
            db.SaveChanges();
            return entity.MaKH;
        }

        public int Login(string username, string password)
        {
            var result = db.KhachHangs.SingleOrDefault(x => x.TaiKhoan == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.MatKhau == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int CheckUsername(string username,string password)
        {
            var result = db.KhachHangs.SingleOrDefault(x => x.TaiKhoan == username);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.MatKhau == password)
                {
                    return 1;
                }
                return -1;
            }
        }

        public int UpdateUser(KhachHang entity)
        {
            try
            {
                var kh = db.KhachHangs.Find(entity.MaKH);
                kh.TenKH = entity.TenKH;
                kh.Email = entity.Email;
                kh.DiaChi = entity.DiaChi;
                kh.DienThoai = entity.DienThoai;
                kh.NgaySinh = entity.NgaySinh;
                kh.TrangThai = entity.TrangThai;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }



    }
}