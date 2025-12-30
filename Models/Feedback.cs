using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Ngaysinh { get; set; }

    public string? Lop { get; set; }

    public string? Coso { get; set; }

    public string? Tel { get; set; }

    public string? Mail { get; set; }

    public string? Ykien { get; set; }

    public string? Detail { get; set; }

    public DateTime? Date { get; set; }

    public int? Active { get; set; }

    public string Code { get; set; } = null!;
}
