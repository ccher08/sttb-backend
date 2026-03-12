using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [Route("api/kompetensi")]
    [ApiController]
    public class CompetencyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompetencyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.CompetencyGroups
                .Include(c => c.Items)
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("program/{programId}")]
        public async Task<IActionResult> GetByProgram(int programId)
        {
            var data = await _context.CompetencyGroups
                .Include(c => c.Items)
                .Where(c => c.ProgramStudiId == programId)
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompetencyGroup group)
        {
            _context.CompetencyGroups.Add(group);
            await _context.SaveChangesAsync();
            return Ok(group);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CompetencyGroup group)
        {
            _context.CompetencyGroups.Update(group);
            await _context.SaveChangesAsync();
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _context.CompetencyGroups.FindAsync(id);
            if (group == null) return NotFound();

            _context.CompetencyGroups.Remove(group);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}