using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class RentHistory
{
    public int HistoryId { get; set; }

    public int ReaderId { get; set; }

    public int RentId { get; set; }

    public virtual Reader Reader { get; set; } = null!;

    public virtual RentBook Rent { get; set; } = null!;
}
