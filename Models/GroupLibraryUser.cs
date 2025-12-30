using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class GroupLibraryUser
{
    public long Id { get; set; }

    public int? GroupLibraryId { get; set; }

    public int? UserId { get; set; }

    public bool? Check { get; set; }

    public virtual GroupLibrary? GroupLibrary { get; set; }

    public virtual User? User { get; set; }
}
