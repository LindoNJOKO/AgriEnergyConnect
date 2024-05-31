using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? PoductName { get; set; }

    public string? ProductDes { get; set; }

    public string? Catergry { get; set; }

    public int? FarmerId { get; set; }

    public virtual Farmer? Farmer { get; set; }
}
