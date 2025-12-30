using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class GroupNewsUser
{
    public long Id { get; set; }

    public int? GroupNewsId { get; set; }

    public int? UserId { get; set; }

    public bool? Check { get; set; }

    public virtual GroupNews? GroupNews { get; set; }

    public virtual User? User { get; set; }
}
