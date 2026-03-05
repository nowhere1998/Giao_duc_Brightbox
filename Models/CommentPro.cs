using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyShop.Models;
public partial class CommentPro
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Comment1 { get; set; }
    public DateTime? Date { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn đánh giá")]
    public int? Rate { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn học viên")]
    public int? CustomerId { get; set; }
    [Required(ErrorMessage = "Vui lòng chọn khóa học")]
    public int? ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public int? Active { get; set; }
    public virtual Product? Product { get; set; }
    public virtual Customer? Customer { get; set; }


}