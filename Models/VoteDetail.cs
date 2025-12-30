using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class VoteDetail
{
    public long Id { get; set; }

    public int? Point { get; set; }

    public string? Ip { get; set; }

    public DateTime? Date { get; set; }

    public int? VoteId { get; set; }

    public long? NewsId { get; set; }

    public virtual News? News { get; set; }

    public virtual Vote? Vote { get; set; }
}
