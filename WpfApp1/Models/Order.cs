using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public float TotalAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
