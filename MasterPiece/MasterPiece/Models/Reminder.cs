using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Reminder
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateOnly OccasionDate { get; set; }

    public bool? ReminderSent { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
