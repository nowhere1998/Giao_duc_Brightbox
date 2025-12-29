using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Customer
    {
        [Display(Name = "Mã khách hàng")]
        public long CustomerId { get; set; }

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Mật khẩu")]
        public string? PasswordHash { get; set; }

        [Display(Name = "Giới tính")]
        public string? Gender { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateOnly? Birthday { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Địa chỉ giao hàng")]
        public string? ShippingAddress { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
