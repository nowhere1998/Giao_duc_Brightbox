using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Permission
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ModuleId { get; set; }

    public string? Right { get; set; }

    public virtual Module? Module { get; set; }

    public virtual User? User { get; set; }
}
