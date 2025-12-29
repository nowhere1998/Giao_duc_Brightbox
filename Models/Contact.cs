using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Contact
    {
        [Display(Name = "Mã liên hệ")]
        public long Id { get; set; }

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Tin nhắn")]
        public string? Message { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Trạng thái")]
        public string? Status { get; set; } 
    }
}
