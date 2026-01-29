namespace HolboxOne.Shared.Entities;

/// <summary>
/// Representa un servicio o item adicional que puede agregarse a Tours o Trips
/// Ejemplos: Boleto de Ferry, Comida, Bebida, Equipo de Snorkel, Seguro
/// </summary>
public class ServiceItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public ServiceItemCategory Category { get; set; }
    public bool IsActive { get; set; } = true;

    // Navegación - Items pueden asociarse a Tours o Trips
    public ICollection<TourServiceItem> TourServiceItems { get; set; } = new List<TourServiceItem>();
    public ICollection<TripServiceItem> TripServiceItems { get; set; } = new List<TripServiceItem>();
}

public enum ServiceItemCategory
{
    Transport = 0,      // Ferry, Taxi adicional
    Food = 1,           // Comida, Bebidas
    Equipment = 2,      // Snorkel, Kayak
    Insurance = 3,      // Seguros
    Other = 4
}

/// <summary>
/// Relación Many-to-Many entre Tour y ServiceItem
/// </summary>
public class TourServiceItem
{
    public int TourId { get; set; }
    public Tour Tour { get; set; } = null!;
    
    public int ServiceItemId { get; set; }
    public ServiceItem ServiceItem { get; set; } = null!;
    
    public bool IsRequired { get; set; } // true si es obligatorio, false si es opcional
    public int Quantity { get; set; } = 1; // Cantidad incluida por defecto
}

/// <summary>
/// Relación Many-to-Many entre TripTemplate y ServiceItem
/// </summary>
public class TripServiceItem
{
    public int TripTemplateId { get; set; }
    public TripTemplate TripTemplate { get; set; } = null!;
    
    public int ServiceItemId { get; set; }
    public ServiceItem ServiceItem { get; set; } = null!;
    
    public bool IsRequired { get; set; }
    public int Quantity { get; set; } = 1;
}

/// <summary>
/// Items seleccionados por el cliente en una reserva de Tour
/// </summary>
public class TourBookingServiceItem
{
    public int Id { get; set; }
    public int TourBookingId { get; set; }
    public TourBooking TourBooking { get; set; } = null!;
    
    public int ServiceItemId { get; set; }
    public ServiceItem ServiceItem { get; set; } = null!;
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Precio al momento de la compra
    public decimal TotalPrice { get; set; }
}

/// <summary>
/// Items seleccionados por el cliente en una reserva de Trip
/// </summary>
public class BookingServiceItem
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
    
    public int ServiceItemId { get; set; }
    public ServiceItem ServiceItem { get; set; } = null!;
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
