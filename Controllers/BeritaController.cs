using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

[ApiController]
[Route("api/berita")]
public class BeritaController : ControllerBase
{
    private readonly AppDbContext _context;

    public BeritaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _context.Berita
            .OrderByDescending(b => b.Date)
            .ToListAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _context.Berita.FindAsync(id);
        if (data == null) return NotFound();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Berita berita)
    {
        _context.Berita.Add(berita);
        await _context.SaveChangesAsync();
        return Ok(berita);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Berita berita)
    {
        var existing = await _context.Berita.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Title = berita.Title;
        existing.Excerpt = berita.Excerpt;
        existing.Image = berita.Image;
        existing.Date = berita.Date;
        existing.Category = berita.Category;
        existing.Author = berita.Author;

        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var data = await _context.Berita.FindAsync(id);
        if (data == null) return NotFound();

        _context.Berita.Remove(data);
        await _context.SaveChangesAsync();
        return Ok();
    }
}