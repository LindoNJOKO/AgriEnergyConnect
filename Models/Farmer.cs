using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

public partial class Farmer
{
    public int FarmerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? Cell { get; set; }

    public string? City { get; set; }

    public string? FarmerPassword { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
