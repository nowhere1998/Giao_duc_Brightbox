using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class TbGiatriDangky
{
    public long Idgt { get; set; }

    public int? Idtt { get; set; }

    public string? Giatri { get; set; }

    public DateTime? NgayDk { get; set; }

    public virtual TbTtdangky? IdttNavigation { get; set; }
}
