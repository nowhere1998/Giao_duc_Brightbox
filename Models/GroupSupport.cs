using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class GroupSupport
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Ord { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public virtual ICollection<Support> Supports { get; set; } = new List<Support>();
}
