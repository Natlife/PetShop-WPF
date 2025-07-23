using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public int ServiceId { get; set; }

    public DateTime BookingTime { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public int? PetId { get; set; }

    public virtual Pet? Pet { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
