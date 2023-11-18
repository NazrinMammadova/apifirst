using api1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Intro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var existCategory = _context.Categories.Where(c=>!c.IsDelete).FirstOrDefault(c=>c.Id == id);
            if (existCategory != null) return NotFound();
            return Ok(existCategory);
        }
    }
}
