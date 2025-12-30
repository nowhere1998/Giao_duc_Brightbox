using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class ListDangkyhocCackhoa
{
    public string? Hoten { get; set; }

    public string? Namsinh { get; set; }

    public string? Dienthoai { get; set; }

    public string? Email { get; set; }

    public string? Sinhvientruong { get; set; }

    public string? Khoa { get; set; }

    public int? Dienkhoan { get; set; }

    public int? Nhanthongtin { get; set; }

    public string? Nguon { get; set; }

    public DateTime? Ngaydangky { get; set; }

    public string Khoahoc { get; set; } = null!;
}
