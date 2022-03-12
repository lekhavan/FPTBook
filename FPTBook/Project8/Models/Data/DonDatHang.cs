namespace WebBanSach.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            ChiTietDDHs = new HashSet<ChiTietDDH>();
        }

        [Key]
        [Display(Name = "Order ID")]
        public int MaDDH { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Order Date")]
        public DateTime NgayDat { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Delivery Date")]
        public DateTime NgayGiao { get; set; }

        [Display(Name = "Status")]
        public bool TinhTrang { get; set; }

        public int MaKH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDDH> ChiTietDDHs { get; set; }

        public virtual KhachHang KhachHang { get; set; }

    }
}
