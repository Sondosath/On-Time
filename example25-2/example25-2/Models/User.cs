using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace example25_2.Models;

public partial class User
{
    public int Id { get; set; }



    public string? Name { get; set; }



    [Required(ErrorMessage = "Email is required")]

    public string Email { get; set; } = null!;


    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;



    public string? Role { get; set; }
}
