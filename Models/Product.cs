using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal?SalePrice { get; set; }

    public int? Index { get; set; }

    public int? Active { get; set; }

    public int? CategoryId { get; set; }

    public string? Lang { get; set; }
    public int? FacultyID { get; set; }     // khóa ngoại
    public virtual Faculty? Faculty { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<CommentPro> CommentPros { get; set; }
    = new List<CommentPro>();

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
