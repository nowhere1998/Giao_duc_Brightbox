using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class GroupNews
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Level { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Keyword { get; set; }

    public int? Ord { get; set; }

    public int? Priority { get; set; }

    public int? Index { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public int? Type1 { get; set; }

    public int? Type2 { get; set; }

    public int? Type3 { get; set; }

    public int? Type4 { get; set; }

    public int? Type5 { get; set; }

    public string? Hinhanh { get; set; }

    public virtual ICollection<GroupNewsUser> GroupNewsUsers { get; set; } = new List<GroupNewsUser>();

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
