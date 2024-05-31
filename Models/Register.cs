using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

public partial class Register
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? Cell { get; set; }

    public string? City { get; set; }

    public string? UserPassword { get; set; }
    public string Role { get; internal set; }
}
