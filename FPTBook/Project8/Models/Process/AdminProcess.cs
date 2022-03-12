using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanSach.Models.Data;

namespace WebBanSach.Models.Process
{
    public class AdminProcess
    {

        BSDBContext db = null;

        //constructor
        public AdminProcess()
        {
            db = new BSDBContext();
        }

        public int Login(string username, string password)
        {
            var result = db.Admins.SingleOrDefault(x => x.TaiKhoan == username);
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


        #region lấy mã

     
        public Admin GetIdAdmin(int id)
        {
            return db.Admins.Find(id);
        }

      
        public Sach GetIdBook(int id)
        {
            return db.Saches.Find(id);
        }

       
        public TheLoai GetIdCategory(int id)
        {
            return db.TheLoais.Find(id);
        }

        
        public TacGia GetIdAuthor(int id)
        {
            return db.TacGias.Find(id);
        }

        
        public NhaXuatBan GetIdPublish(int id)
        {
            return db.NhaXuatBans.Find(id);
        }

       
        public KhachHang GetIdCustomer(int id)
        {
            return db.KhachHangs.Find(id);
        }

        public DonDatHang GetIdOrder(int id)
        {
            return db.DonDatHangs.Find(id);
        }

       
        public LienHe GetIdContact(int id)
        {
            return db.LienHes.Find(id);
        }

        #endregion



        #region thể loại

   
        public List<TheLoai> ListAllCategory()
        {
            return db.TheLoais.OrderBy(x => x.MaLoai).ToList();
        }

   
        public int InsertCategory(TheLoai entity)
        {
            db.TheLoais.Add(entity);
            db.SaveChanges();
            return entity.MaLoai;
        }

        public int UpdateCategory(TheLoai entity)
        {
            try
            {
                var tl = db.TheLoais.Find(entity.MaLoai);
                tl.TenLoai = entity.TenLoai;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                var tl = db.TheLoais.Find(id);
                db.TheLoais.Remove(tl);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion



        #region tác giả


        public List<TacGia> ListAllAuthor()
        {
            return db.TacGias.OrderBy(x => x.MaTG).ToList();
        }

        public int InsertAuthor(TacGia entity)
        {
            db.TacGias.Add(entity);
            db.SaveChanges();
            return entity.MaTG;
        }


        public int UpdateAuthor(TacGia entity)
        {
            try
            {
                var tg = db.TacGias.Find(entity.MaTG);
                tg.TenTG = entity.TenTG;
                tg.QueQuan = entity.QueQuan;
                tg.NgaySinh = entity.NgaySinh;
                tg.NgayMat = entity.NgayMat;
                tg.TieuSu = entity.TieuSu;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteAuthor(int id)
        {
            try
            {
                var tg = db.TacGias.Find(id);
                db.TacGias.Remove(tg);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion




        #region nhà xuất bản


        public List<NhaXuatBan> ListAllPublish()
        {
            return db.NhaXuatBans.OrderBy(x => x.MaNXB).ToList();
        }


        public int InsertPublish(NhaXuatBan entity)
        {
            db.NhaXuatBans.Add(entity);
            db.SaveChanges();
            return entity.MaNXB;
        }

        public int UpdatePublish(NhaXuatBan entity)
        {
            try
            {
                var nxb = db.NhaXuatBans.Find(entity.MaNXB);
                nxb.TenNXB = entity.TenNXB;
                nxb.DiaChi = entity.DiaChi;
                nxb.DienThoai = entity.DienThoai;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool DeletePublish(int id)
        {
            try
            {
                var nxb = db.NhaXuatBans.Find(id);
                db.NhaXuatBans.Remove(nxb);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion




        #region sách


        public List<Sach> ListAllBook()
        {
            return db.Saches.OrderBy(x => x.MaSach).ToList();
        }

 
        public int InsertBook(Sach entity)
        {
            db.Saches.Add(entity);
            db.SaveChanges();
            return entity.MaSach;
        }

        public int UpdateBook(Sach entity)
        {
            try
            {
                var sach = db.Saches.Find(entity.MaSach);
                sach.MaLoai = entity.MaLoai;
                sach.MaNXB = entity.MaNXB;
                sach.MaTG = entity.MaTG;
                sach.TenSach = entity.TenSach;
                sach.GiaBan = entity.GiaBan;
                sach.Mota = entity.Mota;
                sach.NguoiDich = entity.NguoiDich;
                sach.AnhBia = entity.AnhBia;
                sach.NgayCapNhat = entity.NgayCapNhat;
                sach.SoLuongTon = entity.SoLuongTon;
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool DeleteBook(int id)
        {
            try
            {
                var sach = db.Saches.Single(x => x.MaSach == id);
                db.Saches.Remove(sach);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion



        #region phản hồi khách hàng


        public List<LienHe> ShowListContact()
        {
            return db.LienHes.OrderBy(x => x.MaLH).ToList();
        }


        public bool deleteContact(int id)
        {
            try
            {
                var contact = db.LienHes.Find(id);
                db.LienHes.Remove(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

  


        public List<KhachHang> ListUser()
        {
            return db.KhachHangs.OrderBy(x => x.MaKH).ToList();
        }

 
        public bool DeleteUser(int id)
        {
            try
            {
                var user = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}