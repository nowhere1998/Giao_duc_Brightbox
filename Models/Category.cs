using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Level { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Keyword { get; set; }

    [Display(Name = "Thứ tự")]
    [Required(ErrorMessage = "Thứ tự không được để trống")]
    public int? Ord { get; set; }

    public string? Image { get; set; }

    public int? Index { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
