using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class ContactU
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
