using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Dealer
    {
        [Display(Name = "Mã nhà cung cấp")]
        public int Id { get; set; }

        [Display(Name = "Tên nhà cung cấp")]
        public string? Name { get; set; }

        [Display(Name = "Người liên hệ")]
        public string? ContactPerson { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Khu vực")]
        public string? Region { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
