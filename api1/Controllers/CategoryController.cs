using api1.Data;
using api1.Dtos.CategoryDtos;
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

        [HttpGet]
        public IActionResult GetAll(int page = 1, int take = 3, string search = null)
        {
            var query = _context.Categories.Where(c => !c.IsDelete);
            int totalCount = query.Count();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            }
            CategoryListDto categoryListDto = new()
            {
                TotalCount = totalCount,
                Page = page,
                Items = query.Select(c => new CategoryReturnDto()
                {
                    Name = c.Name,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    DeleteDate = c.DeleteDate
                })
                .Skip((page - 1) * take)
                .Take(take)
                .ToList()
            };
            return Ok(categoryListDto);
        }
        [HttpPost]
        public IActionResult Create(CategoryCreateDto categoryCreateDto)
        {
            if (_context.Categories.Any(c => !c.IsDelete && c.Name.ToLower() == categoryCreateDto.Name.ToLower()))
                return BadRequest();
            _context.Categories.Add(new() { Name = categoryCreateDto.Name });
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id != categoryUpdateDto.Id) return BadRequest();
            var existCategory = _context.Categories
                .Where(c => !c.IsDelete)
                .FirstOrDefault(c => c.Id == id);
            bool existName = _context.Categories
                .Any(c => !c.IsDelete && c.Name.ToLower() == categoryUpdateDto.Name.ToLower() && c.Id != id);
            if (existName) return BadRequest();
            existCategory.Name = categoryUpdateDto.Name;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existCategory = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (existCategory == null) return NotFound();
            existCategory.IsDelete = true;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
