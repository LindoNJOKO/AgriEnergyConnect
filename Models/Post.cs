using System;
using System.Collections.Generic;

namespace AgriEnergyConnect.Models;

public partial class Post
{
    public int PostId { get; set; }

    public string? PostDes { get; set; }

    public int? FarmerId { get; set; }

    public virtual Farmer? Farmer { get; set; }
}
