namespace WebBanSach.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonDatHangs = new HashSet<DonDatHang>();
        }

        [Key]
        [Display(Name ="Customer ID")]
        public int MaKH { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Cannot be left blank")]
        [Display(Name = "Customer Name")]
        public string TenKH { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Email cannot be left blank")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(250)]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address cannot be left blank")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number cannot be left blank")]
        [DataType(DataType.PhoneNumber)]
        public string DienThoai { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NgaySinh { get; set; }
         
        [Required(ErrorMessage = "Account cannot be left blank")]
        [StringLength(50)]
        [Display(Name = "Account")]
        public string TaiKhoan { get; set; }

        [Required(ErrorMessage = "Password cannot be left blank")]
        [StringLength(50)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        [Display(Name = "Create Date")]
        [DataType(DataType.DateTime)]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Status")]
        public bool TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }
    }
}
