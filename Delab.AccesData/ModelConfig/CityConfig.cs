using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delab.AccesData.ModelConfig;

public class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(e => e.CityId); // le estamos diciendo a la base de datos que este es el campo identity
        builder.HasIndex(e => new { e.Name, e.StateId }).IsUnique(); // Condicion para que no se repita el nombre del pais

        builder.HasOne(e => e.State).WithMany(e => e.Cities).OnDelete(DeleteBehavior.Restrict);
    }
}
