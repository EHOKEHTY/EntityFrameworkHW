using System;
using System.Collections.Generic;

namespace ContextLibrery;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int DocumentTypeId { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual ICollection<RentBook> RentBooks { get; set; } = new List<RentBook>();

    public virtual ICollection<RentHistory> RentHistories { get; set; } = new List<RentHistory>();
}
