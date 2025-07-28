using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public float Price { get; set; }

    public int Duration { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
