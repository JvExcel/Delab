using System.Reflection;
using HolboxOne.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HolboxOne.AccesData.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    // solo se van a setear las tablas que coloque aqui
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<City> Cities => Set<City>();

    // Tours
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<TourBooking> TourBookings => Set<TourBooking>();

    // Trips & Bookings
    public DbSet<TripTemplate> TripTemplates => Set<TripTemplate>();
    public DbSet<TripSegmentTemplate> TripSegmentTemplates => Set<TripSegmentTemplate>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingSegment> BookingSegments => Set<BookingSegment>();

    // Service Items
    public DbSet<ServiceItem> ServiceItems => Set<ServiceItem>();
    public DbSet<TourServiceItem> TourServiceItems => Set<TourServiceItem>();
    public DbSet<TripServiceItem> TripServiceItems => Set<TripServiceItem>();
    public DbSet<TourBookingServiceItem> TourBookingServiceItems => Set<TourBookingServiceItem>();
    public DbSet<BookingServiceItem> BookingServiceItems => Set<BookingServiceItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar claves compuestas para tablas de unión
        modelBuilder.Entity<TourServiceItem>()
            .HasKey(tsi => new { tsi.TourId, tsi.ServiceItemId });

        modelBuilder.Entity<TripServiceItem>()
            .HasKey(tsi => new { tsi.TripTemplateId, tsi.ServiceItemId });

        // Configurar DeleteBehavior para evitar múltiples cascade paths (SQL Server)
        // BookingSegment -> TripSegmentTemplate: Restrict en lugar de Cascade
        modelBuilder.Entity<BookingSegment>()
            .HasOne(bs => bs.SegmentTemplate)
            .WithMany()
            .HasForeignKey(bs => bs.SegmentTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        // TourBooking -> Tour: Restrict para evitar conflictos
        modelBuilder.Entity<TourBooking>()
            .HasOne(tb => tb.Tour)
            .WithMany(t => t.Bookings)
            .HasForeignKey(tb => tb.TourId)
            .OnDelete(DeleteBehavior.Restrict);

        // Booking -> TripTemplate: Restrict
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.TripTemplate)
            .WithMany(tt => tt.Bookings)
            .HasForeignKey(b => b.TripTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        // TripSegmentTemplate -> TripTemplate: Cascade (esta es la principal)
        modelBuilder.Entity<TripSegmentTemplate>()
            .HasOne(tst => tst.TripTemplate)
            .WithMany(tt => tt.Segments)
            .HasForeignKey(tst => tst.TripTemplateId)
            .OnDelete(DeleteBehavior.Cascade);

        // para tomar los calores de configuracion
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
