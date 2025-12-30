using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class TbValueCombo
{
    public int Id { get; set; }

    public int? Idtt { get; set; }

    public string? Giatri { get; set; }

    public virtual TbTtdangky? IdttNavigation { get; set; }
}
