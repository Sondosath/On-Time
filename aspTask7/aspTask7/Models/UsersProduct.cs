using System;
using System.Collections.Generic;

namespace aspTask7.Models;

public partial class UsersProduct
{
    public int? UsersId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? Users { get; set; }
}
