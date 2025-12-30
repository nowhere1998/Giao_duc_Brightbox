using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class TbSukien
{
    public int MaSk { get; set; }

    public string? Tensukien { get; set; }

    public string? Noidung { get; set; }

    public string? NguonLink { get; set; }

    public int? Hienthi { get; set; }

    public int? Accid { get; set; }

    public long? Iviews { get; set; }

    public string? Metatitle { get; set; }

    public string? Metakeyword { get; set; }

    public string? Metadescription { get; set; }

    public string? Tag { get; set; }

    public string? Keyname { get; set; }

    public virtual ICollection<TbTtdangky> TbTtdangkies { get; set; } = new List<TbTtdangky>();
}
