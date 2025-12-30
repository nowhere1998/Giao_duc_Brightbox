using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Member
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public int? GroupMemberId { get; set; }

    public int? Active { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual GroupMember? GroupMember { get; set; }
}
