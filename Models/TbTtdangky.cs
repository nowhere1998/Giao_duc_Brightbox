using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class TbTtdangky
{
    public int Idtt { get; set; }

    public string? Nhan { get; set; }

    public int? Kieudieukhien { get; set; }

    public int? Rong { get; set; }

    public int? Cao { get; set; }

    public int? Trong { get; set; }

    public int? Thutu { get; set; }

    public int? MaSk { get; set; }

    public virtual TbSukien? MaSkNavigation { get; set; }

    public virtual ICollection<TbGiatriDangky> TbGiatriDangkies { get; set; } = new List<TbGiatriDangky>();

    public virtual ICollection<TbValueCombo> TbValueCombos { get; set; } = new List<TbValueCombo>();
}
