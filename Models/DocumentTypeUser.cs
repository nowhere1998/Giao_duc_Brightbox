using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class DocumentTypeUser
{
    public long Id { get; set; }

    public int? DocumentTypeId { get; set; }

    public int? UserId { get; set; }

    public bool? Check { get; set; }

    public virtual DocumentType? DocumentType { get; set; }

    public virtual User? User { get; set; }
}
