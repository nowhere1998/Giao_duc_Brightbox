using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Toggle
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Detail { get; set; }

    public long? NewsId { get; set; }

    public int? Ord { get; set; }

    public int? Active { get; set; }

    public virtual News? News { get; set; }
}
