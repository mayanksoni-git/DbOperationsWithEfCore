using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //[HttpGet("")]
        //public IActionResult GetAllCurrencies() 
        //{
        //    var result = _appDbContext.Currencies.ToList();
        //    return Ok(result);
        //}

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var result = await _appDbContext.Currencies
                .Select (x=> new
                {
                    CureencyId=x.Id,
                    CurrencyName=x.Title
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllCurrenciesByIdAsync([FromRoute] int id)
        {
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAllCurrenciesByNameAsync([FromRoute] string name, [FromQuery] string? description)
        {
            var result = await _appDbContext.Currencies
                .Where(x => 
                x.Title == name 
                && (string.IsNullOrEmpty(description) || x.Description==description)
                ).ToListAsync();
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAllCurrenciesByIdsAsync([FromBody] List<int> ids)
        {
            //var ids= new List<int> { 1, 2, 4 };
            var result = await _appDbContext.Currencies
                .Where(x=>ids.Contains(x.Id))
                .Select(x=> new Currency()
                {
                    Id = x.Id,
                    Title = x.Title,
                })
                .ToListAsync();
            return Ok(result);
        }
    }
}
