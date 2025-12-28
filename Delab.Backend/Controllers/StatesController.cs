using Delab.AccesData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers
{
    [Route("api/states")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly DataContext _context;

        public StatesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetListAsync()
        {
            try
            {
                var listItems = await _context.States.OrderBy(x => x.Name).ToListAsync();
                return Ok(listItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<State>> GetAsync(int Id)
        {
            try
            {
                // Caasos de Uso donde se puede buscar un objeto de diferentes formas
                // 1
                var Item = await _context.States.FindAsync(Id);
                return Ok(Item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] State modelo)
        {
            try
            {
                _context.States.Add(modelo);
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
        public async Task<ActionResult<State>> PutAsync(State modelo)
        {
            try
            {
                // Primero Buscamos el Objeto
                var Update = await _context.States.FirstOrDefaultAsync(x => x.StateId == modelo.StateId);

                // Actualizamos la informacion que necesitamos
                Update!.Name = modelo.Name;
                Update.CountryId = modelo.CountryId;

                // Indico que hay que actualizar
                _context.States.Update(Update);

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
                var DeleteItem = await _context.States.FindAsync(Id);

                // Validar si encontro algo o no
                if (DeleteItem == null)
                {
                    return BadRequest("No se encontro esa Monda");
                }

                // Eliminar
                _context.States.Remove(DeleteItem);

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
}
