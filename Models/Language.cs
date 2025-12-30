using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Language
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Folder { get; set; }

    public bool Default { get; set; }

    public string? Image { get; set; }

    public bool Active { get; set; }
}
