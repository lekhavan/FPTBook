namespace WebBanSach.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LienHe")]
    public partial class LienHe
    {
        [Key]
        public int MaLH { get; set; }

        [StringLength(50)]
        [Display(Name ="Family Name")]
        [Required(ErrorMessage = "They cannot be left blank")]
        public string Ho { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Name cannot be left blank")]
        public string Ten { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email cannot be blank")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone number cannot be left blank")]
        [DataType(DataType.PhoneNumber)]
        public string DienThoai { get; set; }

        [StringLength(500)]
        [Display(Name = "Content")]
        [Required(ErrorMessage = "Your content to enter")]
        public string NoiDung { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Update Date")]
        [DataType(DataType.DateTime)]
        public DateTime? NgayCapNhat { get; set; }
    }
}
