namespace WebBanSach.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Display(Name ="ID ADMIN")]
        public int IDAdmin { get; set; }

        [StringLength(50)]
        [Display(Name = "Account")]
        [Required(ErrorMessage ="Enter account")]
        public string TaiKhoan { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        public string MatKhau { get; set; }

        [StringLength(50)]
        [Display(Name = "")]
        public string HoTen { get; set; }

        [Display(Name = "Status")]
        public bool? TrangThai { get; set; }
    }
}
