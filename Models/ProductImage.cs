using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class ProductImage
    {
        [Display(Name = "Mã hình ảnh")]
        public long Id { get; set; }

        [Display(Name = "Sản phẩm")]
        public long ProductId { get; set; }

        [Display(Name = "Đường dẫn hình ảnh")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
