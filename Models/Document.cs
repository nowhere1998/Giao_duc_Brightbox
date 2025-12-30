using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Document
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public string? Info { get; set; }

    public string? File { get; set; }

    public int? Priority { get; set; }

    public int? Active { get; set; }

    public int? TypeId { get; set; }

    public int? MemberId { get; set; }

    public string? Lang { get; set; }

    public virtual Member? Member { get; set; }

    public virtual DocumentType? Type { get; set; }
}
