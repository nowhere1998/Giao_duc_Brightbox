using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string? Sex { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? BirthDate { get; set; }

    public string? Position { get; set; }

    public string? Job { get; set; }

    public string? Department { get; set; }

    public string? Address { get; set; }

    public string? Tel { get; set; }

    public string? Fax { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public string? Letter { get; set; }
}
