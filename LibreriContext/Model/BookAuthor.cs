using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class BookAuthor
{
    public int BookId { get; set; }

    public int AuthorId { get; set; }

    public virtual Author Book { get; set; } = null!;

    public virtual Book BookNavigation { get; set; } = null!;
}
