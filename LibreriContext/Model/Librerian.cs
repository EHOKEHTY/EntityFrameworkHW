using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class Librerian
{
    public int LibrerianId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }
}
