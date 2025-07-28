using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
