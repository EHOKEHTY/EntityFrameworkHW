using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class Librarian
{
    public int LibrarianId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
}
