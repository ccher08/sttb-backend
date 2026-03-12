using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewAboutController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OverviewAboutController(AppDbContext context)
        {
            _context = context;
        }

        // GET all
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.OverviewAbouts.ToListAsync();
            return Ok(data);
        }

        // GET by ProgramStudiId
        [HttpGet("programstudi/{programStudiId}")]
        public async Task<IActionResult> GetByProgramStudi(int programStudiId)
        {
            var data = await _context.OverviewAbouts
                .Where(o => o.ProgramStudiId == programStudiId)
                .ToListAsync();
            return Ok(data);
        }

        // POST
        [HttpPost("batch")]
        public async Task<IActionResult> CreateBatch([FromBody] List<OverviewAbout> reqs)
        {
            _context.OverviewAbouts.AddRange(reqs);
            await _context.SaveChangesAsync();
            return Ok(reqs);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(OverviewAbout about)
        {
            _context.OverviewAbouts.Update(about);
            await _context.SaveChangesAsync();
            return Ok(about);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.OverviewAbouts.FindAsync(id);
            if (data == null) return NotFound();

            _context.OverviewAbouts.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}