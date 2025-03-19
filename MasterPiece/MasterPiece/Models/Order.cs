using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<TrackingDetail> TrackingDetails { get; set; } = new List<TrackingDetail>();

    public virtual User User { get; set; } = null!;
}
