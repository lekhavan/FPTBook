using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanSach.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace WebBanSach.Areas.Admin.Models
{
    public class LoginModel
    {
        //Call a LoginModel to equal with Admin in Models

        [Required(ErrorMessage = "You have not entered Account")]
        [Display(Name ="Account")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "You have not entered Password")]
        [Display(Name = "Password")]
        public string MatKhau { get; set; }

        [Display(Name = "Remember")]
        public bool? GhiNho { get; set; }

        [Display(Name = "Fullname")]
        public string HoTen { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
    }
}