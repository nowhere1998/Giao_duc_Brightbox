using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class DocumentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Ord { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public virtual ICollection<DocumentTypeUser> DocumentTypeUsers { get; set; } = new List<DocumentTypeUser>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
