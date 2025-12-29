using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class GroupNews
    {
        [Display(Name = "Mã nhóm tin")]
        public int Id { get; set; }

        [Display(Name = "Tên nhóm tin")]
        public string? Name { get; set; }

        [Display(Name = "Thẻ tag")]
        public string? Tag { get; set; }

        [Display(Name = "Cấp độ")]
        public string? Level { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Từ khóa")]
        public string? Keyword { get; set; }

        [Display(Name = "Thứ tự")]
        [Required(ErrorMessage = "Thứ tự không được để trống")]
        public int? Ord { get; set; }

        [Display(Name = "Độ ưu tiên")]
        public int? Priority { get; set; }

        [Display(Name = "Chỉ mục")]
        public int? Index { get; set; }

        [Display(Name = "Kích hoạt")]
        public int? Active { get; set; }

        [Display(Name = "Ngôn ngữ")]
        public string? Lang { get; set; }

        [Display(Name = "Loại 1")]
        public int? Type1 { get; set; }

        [Display(Name = "Loại 2")]
        public int? Type2 { get; set; }

        [Display(Name = "Loại 3")]
        public int? Type3 { get; set; }

        [Display(Name = "Loại 4")]
        public int? Type4 { get; set; }

        [Display(Name = "Loại 5")]
        public int? Type5 { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Hinhanh { get; set; }

        public virtual ICollection<News> News { get; set; } = new List<News>();
    }
}
