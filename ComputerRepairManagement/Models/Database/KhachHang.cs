using System;
using System.Collections.Generic;

namespace ComputerRepairManagement.Models.Database;

public partial class KhachHang
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public DateTime NgayTao { get; set; }
}
