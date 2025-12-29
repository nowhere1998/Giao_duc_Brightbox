using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class ProductSpec
    {
        [Display(Name = "Mã thông số")]
        public long Id { get; set; }

        [Display(Name = "Sản phẩm")]
        public long? ProductId { get; set; }

        [Display(Name = "Tên thông số")]
        public string? SpecName { get; set; }

        [Display(Name = "Giá trị thông số")]
        public string? SpecValue { get; set; }

        public virtual Product? Product { get; set; }
    }
}
