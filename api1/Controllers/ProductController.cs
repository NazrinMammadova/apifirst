using api1;
using api1.Data;
using api1.Dtos.ProductDtos;
using api1.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string search, int page=1, int take = 2)
        {
            var query = _context.Products.AsNoTracking()
                .Where(p => !p.IsDelete);
            var totalCount = query.Count();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p=>p.Name.ToLower().Contains(search.ToLower()));
            }
            var products = query
                .Skip((page-1)*take)
                .Take(take)
                .Select(product=>new ProductReturnDto
                {
                    Name = product.Name,
                    SalePrice   = product.SalePrice,
                    CostPrice = product.CostPrice,
                    CreateDate = product.CreateDate,
                    UpdateDate = product.UpdateDate,
                    DeleteDate = product.DeleteDate,
                }).ToList();

            ProductListDto productListDto = new()
            {
                TotalCount = totalCount,
                Items = products
            };
            return Ok(productListDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var existProduct = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id&&!p.IsDelete);
            if (existProduct == null) return NotFound();
            ProductReturnDto productReturnDto = new()
            {
                Name = existProduct.Name,
                SalePrice = existProduct.SalePrice,
                CostPrice = existProduct.CostPrice,
                CreateDate = existProduct.CreateDate,
                DeleteDate = existProduct.DeleteDate,
                UpdateDate = existProduct.UpdateDate,
            };
            return Ok(productReturnDto);
        }
        [HttpPost]
        public IActionResult Create(ProductCreateDto product)
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto product)
        {
            if (id != product.Id) return BadRequest();
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existProduct == null) return NotFound();
            existProduct.Name = product.Name;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CostPrice = product.CostPrice;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public IActionResult ChangeName(int id, string name)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.Name = name;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.IsDelete = true;
            //_context.Products.Remove(existProduct);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
