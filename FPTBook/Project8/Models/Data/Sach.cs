namespace WebBanSach.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDDHs = new HashSet<ChiTietDDH>();
        }

        [Key]
        [Display(Name = "Book ID")]
        public int MaSach { get; set; }

        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "Please select category")]
        public int MaLoai { get; set; }

        [Display(Name = "Publisher ID")]
        [Required(ErrorMessage = "Please select a publisher")]
        public int MaNXB { get; set; }

        [Display(Name = "Author ID")]
        [Required(ErrorMessage = "Please select an author")]
        public int MaTG { get; set; }

        [StringLength(250)]
        [Display(Name = "Book Name")]
        [Required(ErrorMessage = "Please enter book title")]
        public string TenSach { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Price")]
        public decimal? GiaBan { get; set; }

        [StringLength(500)]
        [Display(Name = "Describe")]
        public string Mota { get; set; }

        [StringLength(50)]
        [Display(Name = "Translator")]
        public string NguoiDich { get; set; }

        [StringLength(50)]
        [Display(Name = "Picture")]
        public string AnhBia { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Update Date")]
        public DateTime? NgayCapNhat { get; set; }

        [Display(Name = "Quantity In Stock")]
        public int? SoLuongTon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDDH> ChiTietDDHs { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        public virtual TacGia TacGia { get; set; }
    }
}
