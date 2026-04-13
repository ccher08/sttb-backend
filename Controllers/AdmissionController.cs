using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [ApiController]
    [Route("api/admission")]
    public class AdmissionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdmissionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.AdmissionPackages
                .Include(p => p.ProgramStudi)
                .Include(p => p.AdmissionItems)
                .ToList();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AdmissionPackage model)
        {
            _context.AdmissionPackages.Add(model);
            _context.SaveChanges();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AdmissionPackage model)
        {
            var data = _context.AdmissionPackages
                .Include(p => p.AdmissionItems)
                .FirstOrDefault(p => p.Id == id);

            if (data == null) return NotFound();

            data.ProgramStudiId = model.ProgramStudiId;
            data.Year = model.Year;

            // 🔥 replace items (simple approach)
            _context.AdmissionItems.RemoveRange(data.AdmissionItems);

            if (model.AdmissionItems != null)
            {
                foreach (var item in model.AdmissionItems)
                {
                    data.AdmissionItems.Add(new AdmissionItem
                    {
                        Name = item.Name,
                        Price = item.Price
                    });
                }
            }

            _context.SaveChanges();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _context.AdmissionPackages
                .Include(p => p.AdmissionItems)
                .FirstOrDefault(p => p.Id == id);

            if (data == null) return NotFound();

            _context.AdmissionItems.RemoveRange(data.AdmissionItems);
            _context.AdmissionPackages.Remove(data);

            _context.SaveChanges();

            return Ok();
        }
    }
}