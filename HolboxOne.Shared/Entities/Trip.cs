namespace HolboxOne.Shared.Entities;

public class TripTemplate
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int TotalEstimatedDuration { get; set; } // En minutos
    public decimal BasePrice { get; set; }

    // Navegación
    public ICollection<TripSegmentTemplate> Segments { get; set; } = new List<TripSegmentTemplate>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<TripServiceItem> ServiceItems { get; set; } = new List<TripServiceItem>();
}

public class TripSegmentTemplate
{
    public int Id { get; set; }
    public int TripTemplateId { get; set; }
    public TripTemplate TripTemplate { get; set; } = null!;
    
    public int OrderIndex { get; set; } // 1, 2, 3...
    public SegmentType Type { get; set; }
    public string OriginName { get; set; } = null!;
    public string DestinationName { get; set; } = null!;
    public double? OriginLat { get; set; }
    public double? OriginLng { get; set; }
    public double? DestinationLat { get; set; }
    public double? DestinationLng { get; set; }
    public int EstimatedDuration { get; set; } // En minutos
    public decimal Price { get; set; }
}

public enum SegmentType
{
    Vehicle = 0,  // Van, Taxi, Golf Cart
    Ferry = 1,
    Walk = 2
}

public class Booking
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    
    public int TripTemplateId { get; set; }
    public TripTemplate TripTemplate { get; set; } = null!;
    
    public DateTime ScheduledDate { get; set; }
    public BookingStatus Status { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsRoundTrip { get; set; }
    public string? PickupLocationDetails { get; set; } // Hotel específico, etc.
    public DateTime BookingDate { get; set; }

    // Navegación
    public ICollection<BookingSegment> BookingSegments { get; set; } = new List<BookingSegment>();
    public ICollection<BookingServiceItem> ServiceItems { get; set; } = new List<BookingServiceItem>();
}

public class BookingSegment
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
    
    public int SegmentTemplateId { get; set; }
    public TripSegmentTemplate SegmentTemplate { get; set; } = null!;
    
    public BookingStatus Status { get; set; }
    public string? Provider { get; set; } // Driver asignado, etc.
}
