using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class CateRss
{
    public int Id { get; set; }

    public double? Cateid { get; set; }

    public string? Rsslink { get; set; }

    public string? Beginstring { get; set; }

    public string? Endstring { get; set; }

    public string? Source { get; set; }

    public string? Urlimg { get; set; }

    public string? Urlimgupdate { get; set; }

    public string? Urlimgold { get; set; }

    public string? Ulrimgnew { get; set; }

    public string? Imgfolderpath { get; set; }

    public double? Active { get; set; }
}
