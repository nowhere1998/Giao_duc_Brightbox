using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Advertise
    {
        [Display(Name = "Mã quảng cáo")]
        public int Id { get; set; }

        [Display(Name = "Tên quảng cáo")]
        public string? Name { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Chiều rộng")]
        public int? Width { get; set; }

        [Display(Name = "Chiều cao")]
        public int? Height { get; set; }

        [Display(Name = "Liên kết")]
        public string? Link { get; set; }

        [Display(Name = "Target (mở liên kết)")]
        public string? Target { get; set; }

        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Vị trí")]
        public short? Position { get; set; }

        [Display(Name = "Mã trang")]
        public int? PageId { get; set; }

        [Display(Name = "Lượt nhấp")]
        public int? Click { get; set; }

        [Display(Name = "Thứ tự")]
        public int? Ord { get; set; }

        [Display(Name = "Hoạt động")]
        public bool? Active { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string? Lang { get; set; }
    }
}
