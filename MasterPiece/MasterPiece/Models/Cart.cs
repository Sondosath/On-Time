using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual User User { get; set; } = null!;
}
