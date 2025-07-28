using System;
using System.Collections.Generic;

namespace WpfApp1.Models;

public partial class Pet
{
    public int PetId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string? Breed { get; set; }

    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Color { get; set; }

    public double? Weight { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User User { get; set; } = null!;
}
