using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        //  GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Category
                .Select(c => new
                {
                    c.Id,
                    c.Name
                })
                .ToListAsync();

            return Ok(data);
        }

        //  GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _context.Category
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Name
                })
                .FirstOrDefaultAsync();

            if (data == null) return NotFound();

            return Ok(data);
        }

        //  CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category req)
        {
            //  prevent duplicate name
            var exists = await _context.Category
                .AnyAsync(c => c.Name == req.Name);

            if (exists)
                return BadRequest("Category sudah ada");

            var category = new Category
            {
                Name = req.Name
            };

            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Created", category.Id });
        }

        //  UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category req)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null) return NotFound();

            //  prevent duplicate name
            var exists = await _context.Category
                .AnyAsync(c => c.Name == req.Name && c.Id != id);

            if (exists)
                return BadRequest("Category sudah ada");

            category.Name = req.Name;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Updated" });
        }

        //  DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Category
                .Include(c => c.Medias)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            //  cegah delete kalau masih dipakai
            if (category.Medias != null && category.Medias.Any())
            {
                return BadRequest("Category masih digunakan oleh Media");
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }
    }
}