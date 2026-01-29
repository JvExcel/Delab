using HolboxOne.AccesData.Data;
using HolboxOne.Shared.Entities;

namespace HolboxOne.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountries();
    }

    private async Task CheckCountries()
    {
        // Revisar si Countries esta vacio,si no tiene nada quiere decir que hay que llenarla de informacion
        if (!_context.Countries.Any())
        {
            // 1. COLOMBIA
            _context.Countries.Add(
                new Country
                {
                    Name = "Colombia",
                    CodPhone = "+57",
                    States = new List<State>()
                    {
            new State
            {
                Name = "Antioquia",
                Cities = new List<City>()
                {
                    new City { Name = "Medellín" },
                    new City { Name = "Envigado" },
                    new City { Name = "Itagüí" },
                    new City { Name = "Rionegro" },
                    new City { Name = "Bello" }
                }
            },
            new State
            {
                Name = "Valle del Cauca",
                Cities = new List<City>()
                {
                    new City { Name = "Cali" },
                    new City { Name = "Palmira" },
                    new City { Name = "Buenaventura" },
                    new City { Name = "Buga" },
                    new City { Name = "Tuluá" }
                }
            }
                    }
                }
            );

            // 2. MÉXICO
            _context.Countries.Add(
                new Country
                {
                    Name = "México",
                    CodPhone = "+52",
                    States = new List<State>()
                    {
            new State
            {
                Name = "Chiapas",
                Cities = new List<City>()
                {
                    new City { Name = "Tuxtla Gutiérrez" },
                    new City { Name = "Tapachula" },
                    new City { Name = "San Cristóbal de las Casas" },
                    new City { Name = "Comitán" },
                    new City { Name = "Cintalapa" }
                }
            },
            new State
            {
                Name = "Jalisco",
                Cities = new List<City>()
                {
                    new City { Name = "Guadalajara" },
                    new City { Name = "Zapopan" },
                    new City { Name = "Tlaquepaque" },
                    new City { Name = "Puerto Vallarta" },
                    new City { Name = "Lagos de Moreno" }
                }
            }
                    }
                }
            );

            // 3. ESPAÑA
            _context.Countries.Add(
                new Country
                {
                    Name = "España",
                    CodPhone = "+34",
                    States = new List<State>()
                    {
            new State
            {
                Name = "Madrid",
                Cities = new List<City>()
                {
                    new City { Name = "Madrid" },
                    new City { Name = "Alcalá de Henares" },
                    new City { Name = "Móstoles" },
                    new City { Name = "Getafe" },
                    new City { Name = "Leganés" }
                }
            },
            new State
            {
                Name = "Cataluña",
                Cities = new List<City>()
                {
                    new City { Name = "Barcelona" },
                    new City { Name = "Hospitalet de Llobregat" },
                    new City { Name = "Badalona" },
                    new City { Name = "Tarragona" },
                    new City { Name = "Sabadell" }
                }
            }
                    }
                }
            );

            // guardar los cambios
            await _context.SaveChangesAsync();
        }
    }
}
