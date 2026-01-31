using System;
using System.Collections.Generic;
namespace MyShop.Models;
public partial class CommentPro
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Comment1 { get; set; }
    public DateTime? Date { get; set; }
    public int? Rate { get; set; }
    public int? CustomerId { get; set; }
    public int? ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public virtual Product? Product { get; set; }
    public virtual Customer? Customer { get; set; }


}