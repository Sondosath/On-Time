using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspTask7.Models;

public partial class User
{

    public int Id { get; set; }

    public string? Name { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]

    public string Password { get; set; } = null!;

    public string? Role { get; set; }
}
