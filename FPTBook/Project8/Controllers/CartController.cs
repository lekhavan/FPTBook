using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models.Data;
using WebBanSach.Models.Process;
using WebBanSach.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project8.Models.Data;
using Project8.Models.Process;

namespace WebBanSach.Controllers
{
    public class CartController : Controller
    {

        BSDBContext db = new BSDBContext();
        public static ChiTietDDH ct;

        private const string CartSession = "CartSession";


        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartModel>();
            var sl = 0;
            decimal? total = 0;
            if (cart != null)
            {
                list = (List<CartModel>)cart;
                sl = list.Sum(x => x.Quantity);
                total = list.Sum(x => x.Total);
            }
            ViewBag.Quantity = sl;
            ViewBag.Total = total;
            return View(list);
        }


        public ActionResult CartHeader()
        {
            var cart = Session[CartSession];
            var list = new List<CartModel>();
            if (cart != null)
            {
                list = (List<CartModel>)cart;
            }

            return PartialView(list);
        }

 
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartModel>)Session[CartSession];
   
            sessionCart.RemoveAll(x => x.sach.MaSach == id);
 
            Session[CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

   
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {

            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartModel>>(cartModel);

   
            var sessionCart = (List<CartModel>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.Single(x => x.sach.MaSach == item.sach.MaSach);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            Session[CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

   
        public ActionResult AddItem(int id, int quantity)
        {
 
            var sach = new AdminProcess().GetIdBook(id);


            var cart = Session[CartSession];

            if (cart != null)
            {
                var list = (List<CartModel>)cart;
                if (list.Exists(x => x.sach.MaSach == id))
                {

                    foreach (var item in list)
                    {
                        if (item.sach.MaSach == id)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
           
                    var item = new CartModel();
                    item.sach = sach;
                    item.Quantity = quantity;
                    list.Add(item);
                }

               
                Session[CartSession] = list;
            }
            else
            {
                
                var item = new CartModel();
                item.sach = sach;
                item.Quantity = quantity;
                var list = new List<CartModel>();
                list.Add(item);

            
                Session[CartSession] = list;
            }

            return RedirectToAction("Index");
        }

     
        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult UserInfo()
        {
         
            var model = Session["User"];

            if (ModelState.IsValid)
            {
               
                var result = db.KhachHangs.SingleOrDefault(x => x.TaiKhoan == model);

                return PartialView(result);
            }

            return PartialView();
        }

        [HttpGet]
        public ActionResult Payment()
        {
           
            if (Session["User"] == null || Session["User"].ToString() == "")
            {
                return RedirectToAction("LoginPage", "User");
            }

            if (UserController.khachhangstatic.TrangThai == false)
            {
                return RedirectToAction("ActivationNotice", "User");
            }
            else
            {
                var cart = Session[CartSession];
                var list = new List<CartModel>();
                var sl = 0;
                decimal? total = 0;
                if (cart != null)
                {
                    list = (List<CartModel>)cart;
                    sl = list.Sum(x => x.Quantity);
                    total = list.Sum(x => x.Total);
                }
                ViewBag.Quantity = sl;
                ViewBag.Total = total;
                return View(list);
            }
        }
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [HttpPost]
        public ActionResult Payment(int MaKH,FormCollection f)
        {
            var PMethod = int.Parse(f["PaymentMethod"]);
            var order = new DonDatHang();
            order.NgayDat = DateTime.Now;
            order.NgayGiao = DateTime.Now.AddDays(3);
            order.TinhTrang = true; 
            order.MaKH = MaKH;
            
            try
            {
                if (PMethod == 1)
                {

                    var result1 = new OrderProcess().Insert(order);
                    var cart = (List<CartModel>)Session[CartSession];
                    var result2 = new OderDetailProcess();
                    decimal? total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new ChiTietDDH();
                        orderDetail.MaSach = item.sach.MaSach;
                        orderDetail.MaDDH = result1;
                        orderDetail.SoLuong = item.Quantity;
                        orderDetail.DonGia = item.sach.GiaBan;
                        result2.Insert(orderDetail);

                        total = cart.Sum(x => x.Total);
                        
                    }

                    Session[CartSession] = null;
                    return Redirect("/Cart/Success");
                }
                else
                {
                    var result1 = new OrderProcess().Insert(order);
                    var cart = (List<CartModel>)Session[CartSession];
                    var result2 = new OderDetailProcess();
                    decimal? total = 0;
                    foreach (var item in cart)
                    {
                        var orderDetail = new ChiTietDDH();
                        orderDetail.MaSach = item.sach.MaSach;
                        orderDetail.MaDDH = result1;
                        orderDetail.SoLuong = item.Quantity;
                        orderDetail.DonGia = item.sach.GiaBan;
                        result2.Insert(orderDetail);

                        total = cart.Sum(x => x.Total);
                    }
                    Session[CartSession] = null;
                    return Redirect(ThanhToanMoMo(result1.ToString(), 
                        total.ToString().Substring(0, total.ToString().Length - 5)));
                    

                }
            }
            catch (Exception)
            {
                return Redirect("/Cart/Error");
            }

            return new EmptyResult();

        }

        protected string ThanhToanMoMo(string maDonHang,string tongCong)
        {
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOHDRK20200430";
            string accessKey = "68tVdaHzCcvtfzwH";
            string serectkey = "8AWejATXBF96XL3CqeICtqiiKwheEUAv";
            string orderInfo = "OrderBook";
            string returnUrl = "https://webbansach17dtha3.cf//Cart/Success";
            string notifyurl = "https://webbansach17dtha3.cf/";

            string amount = tongCong;
            string orderid = maDonHang;
            string requestId = maDonHang;
            string extraData = "";

            string rawHash = "partnerCode=" +
                             partnerCode + "&accessKey=" +
                             accessKey + "&requestId=" +
                             requestId + "&amount=" +
                             amount + "&orderId=" +
                             orderid + "&orderInfo=" +
                             orderInfo + "&returnUrl=" +
                             returnUrl + "&notifyUrl=" +
                             notifyurl + "&extraData=" +
                             extraData;

            log.Debug("rawHash = " + rawHash);
            MoMoSecurity crypto = new MoMoSecurity();
       
            string signature = crypto.signSHA256(rawHash, serectkey);
            log.Debug("Signature = " + signature);

     
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };
            log.Debug("Json request to MoMo: " + message.ToString());
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);
            log.Debug("Return from MoMo: " + jmessage.ToString());
            
            return jmessage.GetValue("payUrl").ToString();
            
        }
        public ActionResult Success()
        {
            return View();
        }

        public ActionResult TrackingOder()
        {
            List<DonDatHang> donDatHang = db.DonDatHangs.Where(p => p.MaKH == UserController.khachhangstatic.MaKH).ToList();
            return View(donDatHang);
        }
        public ActionResult TrackingOderDetails( int id)
        {
            ChiTietDDH orderdetail = db.ChiTietDDHs.Find(id);          
            return View(orderdetail);
        }

        public JsonResult loadOrder()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var donDatHang = db.DonDatHangs.ToList();
            
            return Json(new {data= donDatHang }
                , JsonRequestBehavior.AllowGet);
          
        }
    }
}