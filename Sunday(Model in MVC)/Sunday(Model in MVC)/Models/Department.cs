using System;
using System.Collections.Generic;

namespace Sunday_Model_in_MVC_.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Courses { get; set; }

    public string? Instructer { get; set; }

    public int? NumOfStudemts { get; set; }
}
