using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class News
    {
        [Display(Name = "Mã tin tức")]
        public long Id { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Slug")]
        public string? Slug { get; set; }

        [Display(Name = "Tóm tắt")]
        public string? Excerpt { get; set; }

        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Người đăng bài")]
        public long? PostedById { get; set; }

        [Display(Name = "Tác giả")]
        public string? AuthorName { get; set; }

        [Display(Name = "Nhóm tin tức")]
        public int? GroupId { get; set; }

        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }

        [Display(Name = "Ngày xuất bản")]
        public DateTime? PublishedAt { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }
        public string? Hinhanh { get; set; }

        public virtual User? PostedBy { get; set; }

        public virtual GroupNews? Group { get; set; }
    }
}
