using DbOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguagesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //[HttpGet("")]
        //public IActionResult GetAllLanguages() 
        //{
        //    var result = _appDbContext.Languages.ToList();
        //    return Ok(result);
        //}

        [HttpGet("")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var result = await _appDbContext.Languages.ToListAsync();
            return Ok(result);
        }
    }
}
