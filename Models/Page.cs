using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Page
    {
        [Display(Name = "Mã trang")]
        public int Id { get; set; }

        [Display(Name = "Tên trang")]
        public string? Name { get; set; }

        [Display(Name = "Thẻ tag")]
        public string? Tag { get; set; }

        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Chi tiết")]
        public string? Detail { get; set; }

        [Display(Name = "Cấp độ")]
        public string? Level { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Từ khóa")]
        public string? Keyword { get; set; }

        [Display(Name = "Loại")]
        public int? Type { get; set; }

        [Display(Name = "Đường dẫn")]
        public string? Link { get; set; }

        [Display(Name = "Target")]
        public string? Target { get; set; }

        [Display(Name = "Chỉ mục hiển thị")]
        public int? Index { get; set; }

        [Display(Name = "Vị trí")]
        public int? Position { get; set; }

        [Display(Name = "Thứ tự")]
        [Required(ErrorMessage = "Thứ tự không được để trống")]
        public int? Ord { get; set; }

        [Display(Name = "Kích hoạt")]
        public int? Active { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string? Lang { get; set; }

        [Display(Name = "Mã nhóm")]
        public int? GroupId { get; set; }
    }
}
