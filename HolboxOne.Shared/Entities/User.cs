using Microsoft.AspNetCore.Identity;

namespace HolboxOne.Shared.Entities;

public class User : IdentityUser
{
    public string FullName { get; set; } = null!;

    public UserType UserType { get; set; }

    public string? ProfilePicture { get; set; }

    // Navegación
    public ICollection<TourBooking> TourBookings { get; set; } = new List<TourBooking>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

public enum UserType
{
    Client = 0,
    Admin = 1
}
