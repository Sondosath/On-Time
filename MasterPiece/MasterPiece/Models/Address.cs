using System;
using System.Collections.Generic;

namespace MasterPiece.Models;

public partial class Address
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public string Country { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
