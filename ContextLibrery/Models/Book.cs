using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int? PublisherTypeId { get; set; }

    public int? Year { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public virtual PublisherType? PublisherType { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
