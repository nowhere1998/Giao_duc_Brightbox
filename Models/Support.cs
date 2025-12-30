using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Support
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tel { get; set; }

    public int? Type { get; set; }

    public string? Nick { get; set; }

    public int? Ord { get; set; }

    public int? Active { get; set; }

    public int? GroupSupportId { get; set; }

    public string? Lang { get; set; }

    public int? Priority { get; set; }

    public virtual GroupSupport? GroupSupport { get; set; }
}
