using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Library
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Image { get; set; }

    public string? File { get; set; }

    public string? Info { get; set; }

    public int? Priority { get; set; }

    public int? Active { get; set; }

    public int? GroupLibraryId { get; set; }

    public int? MemberId { get; set; }

    public string? Lang { get; set; }

    public virtual GroupLibrary? GroupLibrary { get; set; }
}
