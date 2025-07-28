using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public float Price { get; set; }

    public int Stock { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
