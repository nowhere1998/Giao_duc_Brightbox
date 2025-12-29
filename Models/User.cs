using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class User
    {
        [Display(Name = "Mã người dùng")]
        public long Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string? Username { get; set; } = null!;

        [Display(Name = "Email")]
        public string? Email { get; set; } = null!;

        [Display(Name = "Mật khẩu")]
        public string? PasswordHash { get; set; }

        [Display(Name = "Họ và tên")]
        public string? FullName { get; set; }

        [Display(Name = "Vai trò")]
        public int? Role { get; set; }

        [Display(Name = "Trạng thái")]
        public byte? Status { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<News> News { get; set; } = new List<News>();
    }
}
