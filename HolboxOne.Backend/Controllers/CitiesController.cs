using HolboxOne.AccesData.Data;
using HolboxOne.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HolboxOne.Backend.Controllers;

[Route("api/Cities")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly DataContext _context;

    public CitiesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetListAsync()
    {
        try
        {
            var listItems = await _context.Cities.OrderBy(x => x.Name).ToListAsync();
            return Ok(listItems);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<City>> GetAsync(int Id)
    {
        try
        {
            // Caasos de Uso donde se puede buscar un objeto de diferentes formas
            // 1
            var Item = await _context.Cities.FindAsync(Id);
            return Ok(Item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] City modelo)
    {
        try
        {
            _context.Cities.Add(modelo);
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
    public async Task<ActionResult<City>> PutAsync(City modelo)
    {
        try
        {
            // Primero Buscamos el Objeto
            var Update = await _context.Cities.FirstOrDefaultAsync(x => x.CityId == modelo.CityId);

            // Actualizamos la informacion que necesitamos
            Update!.Name = modelo.Name;
            Update.StateId = modelo.StateId;

            // Indico que hay que actualizar
            _context.Cities.Update(Update);

            // Guardo los Cambios
            await _context.SaveChangesAsync();

            // Retorno el Objeto Completo
            return Ok(Update);
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

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        try
        {
            // Buscar el Registro
            var DeleteItem = await _context.Cities.FindAsync(Id);

            // Validar si encontro algo o no
            if (DeleteItem == null)
            {
                return BadRequest("No se encontro esa Monda");
            }

            // Eliminar
            _context.Cities.Remove(DeleteItem);

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
