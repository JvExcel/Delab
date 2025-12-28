using Delab.AccesData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

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
        var listCountries = await _context.Countries.OrderBy(x=>x.Name).ToListAsync();
        return Ok(listCountries);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<Country>> GetCountry(int Id)
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

    [HttpPost]
    public async Task<IActionResult> PostCountry([FromBody]Country modelo)
    {
        _context.Countries.Add(modelo);
        await _context.SaveChangesAsync();
        return Ok();
    }

}
