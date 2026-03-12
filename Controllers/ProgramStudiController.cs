using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [ApiController]
    [Route("api/program-studi")]
    public class ProgramStudiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgramStudiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.ProgramStudi
                .Include(p => p.MataKuliah)
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var data = await _context.ProgramStudi
                .Include(p => p.Curriculum)
                    .ThenInclude(c => c.Courses)
                .Include(p => p.Abouts)
                .Include(p => p.Requirements)
                .Include(p => p.Competencies)
                    .ThenInclude(c => c.Items)
                .FirstOrDefaultAsync(p => p.Slug == slug);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProgramStudi data)
        {
            _context.ProgramStudi.Add(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProgramStudi data)
        {
            if (id != data.Id)
                return BadRequest();

            var existing = await _context.ProgramStudi.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = data.Name;
            existing.Slug = data.Slug;
            existing.Level = data.Level;
            existing.Description = data.Description;
            existing.TotalCredits = data.TotalCredits;
            existing.Duration = data.Duration;
            existing.Image = data.Image;
            existing.Highlights = data.Highlights;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.ProgramStudi.FindAsync(id);

            if (data == null)
                return NotFound();

            _context.ProgramStudi.Remove(data);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
