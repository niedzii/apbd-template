using kolokwium_niedzii.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kolokwium_niedzii.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CountriesController : ControllerBase
    {
        private readonly ApbdDbContext _context;

        public CountriesController(ApbdDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Countries.Select(country => new CountryData
            {
                IdCountry = country.IdCountry,
                Name = country.Name
            }).ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _context.Countries.Where(country => country.IdCountry == id).Select(country =>
                new CountryData
                {
                    IdCountry = country.IdCountry,
                    Name = country.Name
                }).SingleOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var country = await _context.Countries.SingleOrDefaultAsync(country => country.IdCountry == id);

            if (country is null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CountryData countryData)
        {
            if (await _context.Countries.AnyAsync(country => country.IdCountry == countryData.IdCountry))
            {
                return Conflict();
            }

            await _context.Countries.AddAsync(new Country
            {
                IdCountry = countryData.IdCountry,
                Name = countryData.Name,
            });

            await _context.SaveChangesAsync();
            return Created("created: ", "");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, PutData countryData)
        {
            var country =
                await _context.Countries.SingleOrDefaultAsync(country => country.IdCountry == id);

            if (country is null)
            {
                return NotFound();
            }

            country.Name = countryData.Name;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}