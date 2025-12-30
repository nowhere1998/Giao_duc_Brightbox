using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Advertise
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string? Link { get; set; }

    public string? Target { get; set; }

    public string? Content { get; set; }

    public short? Position { get; set; }

    public int? PageId { get; set; }

    public int? Click { get; set; }

    public int? Ord { get; set; }

    public bool Active { get; set; }

    public string? Lang { get; set; }
}
