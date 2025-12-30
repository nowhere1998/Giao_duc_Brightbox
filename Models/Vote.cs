using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Vote
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Value { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public virtual ICollection<VoteDetail> VoteDetails { get; set; } = new List<VoteDetail>();
}
