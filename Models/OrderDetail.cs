using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class OrderDetail
    {
        [Display(Name = "Mã chi tiết đơn hàng")]
        public long Id { get; set; }

        [Display(Name = "Mã đơn hàng")]
        public long? OrderId { get; set; }

        [Display(Name = "Mã sản phẩm")]
        public long? ProductId { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Thành tiền")]
        public decimal? Total { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        public virtual Order? Order { get; set; }

        public virtual Product? Product { get; set; }
    }
}
