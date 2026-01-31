using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Image { get; set; }

    public string? Content { get; set; }

    public string? Detail { get; set; }

    public DateTime? Date { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Keyword { get; set; }

    public int? Priority { get; set; }

    public int? Index { get; set; }

    public int? Active { get; set; }

    public int? CategoryId { get; set; }

    public string? Lang { get; set; }

    public virtual Category? Category { get; set; }
    public virtual ICollection<CommentPro> CommentPros { get; set; }
    = new List<CommentPro>();

}
