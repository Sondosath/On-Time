using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BIGMVC_project.Models;

public partial class Hr
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? PasswordHash { get; set; }

    public string? Image { get; set; }
}
