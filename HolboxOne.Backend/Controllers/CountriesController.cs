using HolboxOne.AccesData.Data;
using HolboxOne.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HolboxOne.Backend.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        try
        {
            var listCountries = await _context.Countries
                .Include(X => X.States)! // Señalamiento para incluir su realcion mas cercana
                .ThenInclude(x=> x.Cities) // Señalar que debe ingresar mas adentro de la jerarquia, llegando a Stites => Cities
                .OrderBy(x => x.Name).ToListAsync();
            return Ok(listCountries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Country>> GetCountry(int Id)
    {
        try
        {
            // Caasos de Uso donde se puede buscar un objeto de diferentes formas
            // 1
            var Country = await _context.Countries.FindAsync(Id);
            //// 2
            //var IdCountry = await _context.Countries.Where(x => x.IdCountry == Id).FirstOrDefaultAsync();
            //// 3
            //var IdCountry2 = await _context.Countries.FirstOrDefaultAsync(x => x.IdCountry == Id);
            return Ok(Country);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostCountry([FromBody] Country modelo)
    {
        try
        {
            _context.Countries.Add(modelo);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe ese registro");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Country>> PutCountry(Country modelo)
    {
        try
        {
            // Primero Buscamos el Objeto
            var Updatecountry = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == modelo.CountryId);

            // Actualizamos la informacion que necesitamos
            Updatecountry!.Name = modelo.Name;
            Updatecountry.CodPhone = modelo.CodPhone;

            // Indico que hay que actualizar
            _context.Countries.Update(Updatecountry);

            // Guardo los Cambios
            await _context.SaveChangesAsync();

            // Retorno el Objeto Completo
            return Ok(Updatecountry);
        }
        catch(DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe ese registro");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteCountry(int Id)
    {
        try
        {
            // Buscar el Registro
            var DeleteCountry = await _context.Countries.FindAsync(Id);

            // Validar si encontro algo o no
            if (DeleteCountry == null)
            {
                return BadRequest("No se encontro esa Monda");
            }

            // Eliminar
            _context.Countries.Remove(DeleteCountry);

            // guardar Cambio
            await _context.SaveChangesAsync();


            return Ok("Todo Calidad Mijin");
        }
        catch (DbUpdateException dbEx)
        {

            if (dbEx.InnerException!.Message.Contains("REFERENCE"))
            {
                return BadRequest("No puede eliminar el registro porque tiene datos relacionados");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
