using Microsoft.AspNetCore.Mvc;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcaraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AcaraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Acara.ToList());
        }

        [HttpPost]
        public IActionResult Create(Acara acara)
        {
            _context.Acara.Add(acara);
            _context.SaveChanges();

            return Ok(acara);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Acara acara)
        {
            var data = _context.Acara.Find(id);
            if (data == null) return NotFound();

            data.Title = acara.Title;
            data.Date = acara.Date;
            data.Time = acara.Time;

            _context.SaveChanges();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _context.Acara.Find(id);
            if (data == null) return NotFound();

            _context.Acara.Remove(data);
            _context.SaveChanges();

            return Ok();
        }
    }
}