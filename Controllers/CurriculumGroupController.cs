using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [Route("api/jenis-matkul")]
    [ApiController]
    public class CurriculumGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CurriculumGroupController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.CurriculumGroups
                .Include(c => c.Courses)
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CurriculumGroup group)
        {
            _context.CurriculumGroups.Add(group);
            await _context.SaveChangesAsync();

            return Ok(group);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CurriculumGroup group)
        {
            _context.CurriculumGroups.Update(group);
            await _context.SaveChangesAsync();

            return Ok(group);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.CurriculumGroups.FindAsync(id);

            if (data == null)
                return NotFound();

            _context.CurriculumGroups.Remove(data);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}