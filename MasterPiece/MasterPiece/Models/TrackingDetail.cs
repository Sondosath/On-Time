using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class TrackingDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? TrackingNumber { get; set; }

    public string? DeliveryStatus { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
