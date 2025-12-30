using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Post
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? Content { get; set; }

    public string? Detail { get; set; }

    public DateTime? Date { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Keyword { get; set; }

    public int? Priority { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }
}
