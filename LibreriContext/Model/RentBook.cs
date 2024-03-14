using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class RentBook
{
    public int RentId { get; set; }

    public int BookId { get; set; }

    public int ReaderId { get; set; }

    public DateTime RentDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public DateTime DueDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;

    public virtual ICollection<RentHistory> RentHistories { get; set; } = new List<RentHistory>();
}
