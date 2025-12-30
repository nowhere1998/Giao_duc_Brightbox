using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Detail { get; set; }

    public DateTime? Date { get; set; }

    public int? Active { get; set; }

    public long? NewsId { get; set; }

    public int? Type { get; set; }

    public virtual News? News { get; set; }
}
