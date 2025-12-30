using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class GroupMember
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Active { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
