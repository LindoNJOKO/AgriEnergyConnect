using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

public partial class Login
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? UserPassword { get; set; }
}
