using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Module
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Level { get; set; }

    public string? Name { get; set; }

    public string? Page { get; set; }

    public string? Image { get; set; }

    public int? Ord { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
