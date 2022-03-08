namespace WebBanSach.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDDH")]
    public partial class ChiTietDDH
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSach { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Order ID")]
        public int MaDDH { get; set; }

        [Display(Name = "Quantity")]
        public int? SoLuong { get; set; }

        [Display(Name = "Total Price")]
        public decimal? DonGia { get; set; }

        public virtual DonDatHang DonDatHang { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
