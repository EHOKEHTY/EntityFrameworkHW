using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class PublisherType
{
    public int PublisherTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
