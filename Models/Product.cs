using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public partial class Product
    {
        [Display(Name = "Mã sản phẩm")]
        public long Id { get; set; }

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string? Name { get; set; }

        [Display(Name = "Đường dẫn (Slug)")]
        public string? Slug { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public int? DealerId { get; set; }

        [Display(Name = "Giá gốc")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? SalePrice { get; set; }

        [Display(Name = "Hình ảnh chính")]
        public string? Image { get; set; }

        [NotMapped]
        [Display(Name = "Hình ảnh phụ")]
        public string? GalleryImages { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Trạng thái")]
        public string? Status { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Category? Category { get; set; }

        public virtual Dealer? Dealer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        public virtual ICollection<ProductSpec> ProductSpecs { get; set; } = new List<ProductSpec>();
    }
}
