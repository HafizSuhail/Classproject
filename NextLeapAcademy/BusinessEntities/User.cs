﻿using System;
using System.Collections.Generic;

namespace NextLeapAcademy.BusinessEntities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}
