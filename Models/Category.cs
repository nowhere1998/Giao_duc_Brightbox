using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public partial class Category
    {
        [Display(Name = "Mã danh mục")]
        public int Id { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }

        [Display(Name = "Đường dẫn (Slug)")]
        public string? Slug { get; set; }

        [Display(Name = "Tên danh mục")]
        public string? Name { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

        public virtual Category? Parent { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
