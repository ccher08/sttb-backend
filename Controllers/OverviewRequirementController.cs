using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewRequirementController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OverviewRequirementController(AppDbContext context)
        {
            _context = context;
        }

        // GET all
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _context.OverviewRequirements.ToListAsync();
            return Ok(data);
        }

        // GET by ProgramStudiId
        [HttpGet("programstudi/{programStudiId}")]
        public async Task<IActionResult> GetByProgramStudi(int programStudiId)
        {
            var data = await _context.OverviewRequirements
                .Where(o => o.ProgramStudiId == programStudiId)
                .ToListAsync();
            return Ok(data);
        }

        // POST
        [HttpPost("batch")]
        public async Task<IActionResult> CreateBatch([FromBody] List<OverviewRequirement> reqs)
        {
            _context.OverviewRequirements.AddRange(reqs);
            await _context.SaveChangesAsync();
            return Ok(reqs);
        }

        // PUT
        [HttpPut]
        public async Task<IActionResult> Update(OverviewRequirement req)
        {
            _context.OverviewRequirements.Update(req);
            await _context.SaveChangesAsync();
            return Ok(req);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.OverviewRequirements.FindAsync(id);
            if (data == null) return NotFound();

            _context.OverviewRequirements.Remove(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}