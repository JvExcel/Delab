using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delab.AccesData.ModelConfig;

public class CountryConfig : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.IdCountry); // le estamos diciendo a la base de datos que este es el campo identity
        builder.HasIndex(e => e.Name).IsUnique(); // Condicion para que no se repita el nombre del pais
    }
}
