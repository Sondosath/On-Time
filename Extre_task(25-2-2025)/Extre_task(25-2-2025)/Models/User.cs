﻿using System;
using System.Collections.Generic;

namespace Extre_task_25_2_2025_.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
