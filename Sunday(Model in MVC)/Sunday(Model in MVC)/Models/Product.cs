using System;
using System.Collections.Generic;

namespace Sunday_Model_in_MVC_.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }
}
