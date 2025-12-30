using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class News
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Image { get; set; }

    public string? File { get; set; }

    public string? Video { get; set; }

    public string? Content { get; set; }

    public string? Detail { get; set; }

    public DateTime? Date { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Keyword { get; set; }

    public int? Priority { get; set; }

    public int? Index { get; set; }

    public int? Reportage { get; set; }

    public int? Spotlight { get; set; }

    public int? Latest { get; set; }

    public int? Active { get; set; }

    public int? GroupNewsId { get; set; }

    public string? Lang { get; set; }

    public string? Tags { get; set; }

    public int? Comment { get; set; }

    public int? Register { get; set; }

    public string? RegisterLink { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual GroupNews? GroupNews { get; set; }

    public virtual ICollection<Toggle> Toggles { get; set; } = new List<Toggle>();

    public virtual ICollection<VoteDetail> VoteDetails { get; set; } = new List<VoteDetail>();
}
