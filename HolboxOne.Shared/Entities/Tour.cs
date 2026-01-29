namespace HolboxOne.Shared.Entities;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public double LocationLat { get; set; }
    public double LocationLng { get; set; }
    public int MaxSlots { get; set; }
    public int Duration { get; set; } // En minutos
    public DateTime StartDateTime { get; set; }

    // Navegación
    public ICollection<TourBooking> Bookings { get; set; } = new List<TourBooking>();
    public ICollection<TourServiceItem> ServiceItems { get; set; } = new List<TourServiceItem>();
}

public class TourBooking
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public Tour Tour { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    
    public int TicketCount { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime BookingDate { get; set; }

    // Navegación
    public ICollection<TourBookingServiceItem> ServiceItems { get; set; } = new List<TourBookingServiceItem>();
}

public enum BookingStatus
{
    Pending = 0,
    Paid = 1,
    Confirmed = 2,
    Completed = 3,
    Cancelled = 4
}
