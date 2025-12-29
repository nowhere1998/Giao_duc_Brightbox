using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Order
    {
        [Display(Name = "Mã đơn hàng")]
        public long Id { get; set; }

        [Display(Name = "Mã đơn (Order Code)")]
        public string? OrderCode { get; set; }

        [Display(Name = "Mã khách hàng")]
        public long? CustomerId { get; set; }

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        public string? ShippingAddress { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Note { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string? PaymentMethod { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string? PaymentStatus { get; set; }

        [Display(Name = "Trạng thái đơn hàng")]
        public string? OrderStatus { get; set; }

        [Display(Name = "Tạm tính")]
        public decimal? SubtotalAmount { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal? TotalAmount { get; set; }

        [Display(Name = "Giảm giá")]
        public decimal? DiscountAmount { get; set; }

        [Display(Name = "Phí vận chuyển")]
        public decimal? ShippingFee { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
