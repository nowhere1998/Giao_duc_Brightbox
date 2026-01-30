using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public partial class Page
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Content { get; set; }

    public string? Detail { get; set; }

    public string? Level { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Keyword { get; set; }

    public int? Type { get; set; }

    public string? Link { get; set; }

    public string? Target { get; set; }

    public int? Index { get; set; }

    public int? Position { get; set; }

    [Display(Name = "Thứ tự")]
    [Required(ErrorMessage = "Thứ tự không được để trống")]
    public int? Ord { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public int? GroupId { get; set; }
}
