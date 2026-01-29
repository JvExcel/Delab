using HolboxOne.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolboxOne.AccesData.ModelConfig;

public class StateConfig : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.StateId); // le estamos diciendo a la base de datos que este es el campo identity
        builder.HasIndex(e => new { e.Name,e.CountryId}).IsUnique(); // Condicion para que no se repita el nombre del pais pero compuesto donde se puede tener varias condiciones

        // Hay que protejer de el borrado en cascada
        builder.HasOne(e => e.Country).WithMany(e => e.States).OnDelete(DeleteBehavior.Restrict);
    }
}
