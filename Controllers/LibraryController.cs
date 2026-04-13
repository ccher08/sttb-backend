using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibraryController(AppDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET LIST + SEARCH + FILTER + PAGINATION
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? category,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 6)
        {
            var query = _context.Libraries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Title.Contains(search) ||
                    x.Author.Contains(search) ||
                    x.Isbn.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(category) && category != "Semua Koleksi")
            {
                query = query.Where(x => x.Category == category);
            }

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.ReleaseDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Slug,
                    x.Author,
                    x.Category,
                    x.Description,
                    Cover = x.CoverUrl,
                    x.Isbn,
                    x.ReleaseDate
                })
                .ToListAsync();

            return Ok(new
            {
                total,
                page,
                pageSize,
                data
            });
        }

        // =========================
        // GET DETAIL BY SLUG
        // =========================
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var book = await _context.Libraries
                .Where(x => x.Slug == slug)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.Slug,
                    x.Author,
                    x.Description,
                    x.Category,
                    x.Isbn,
                    x.ReleaseDate,
                    Cover = x.CoverUrl,
                    x.FileUrl
                })
                .FirstOrDefaultAsync();

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LibraryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return BadRequest(new { message = "Title wajib diisi" });

            if (request.Cover == null || request.File == null)
                return BadRequest(new { message = "Cover dan file PDF wajib diupload" });

            if (request.Cover.Length > 2 * 1024 * 1024)
                return BadRequest(new { message = "Ukuran cover maksimal 2MB" });

            if (request.File.Length > 10 * 1024 * 1024)
                return BadRequest(new { message = "Ukuran PDF maksimal 10MB" });

            var allowedImageTypes = new[] { "image/jpeg", "image/png", "image/jpg" };

            if (!allowedImageTypes.Contains(request.Cover.ContentType))
                return BadRequest(new { message = "Cover harus JPG/PNG" });

            if (request.File.ContentType != "application/pdf")
                return BadRequest(new { message = "File harus PDF" });

            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var slug = GenerateSlug(request.Title);
            if (await _context.Libraries.AnyAsync(x => x.Slug == slug))
                slug += "-" + Guid.NewGuid().ToString().Substring(0, 5);

            var coverName = Guid.NewGuid() + Path.GetExtension(request.Cover.FileName);
            var coverPath = Path.Combine("wwwroot/uploads/covers", coverName);

            var fileName = Guid.NewGuid() + ".pdf";
            var filePath = Path.Combine("wwwroot/uploads/files", fileName);

            try
            {
                using (var stream = new FileStream(coverPath, FileMode.Create))
                {
                    await request.Cover.CopyToAsync(stream);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var library = new Library
                {
                    Title = request.Title,
                    Author = request.Author,
                    Description = request.Description,
                    Category = request.Category,
                    Isbn = request.Isbn,
                    ReleaseDate = request.ReleaseDate,
                    CoverUrl = $"{baseUrl}/uploads/covers/{coverName}",
                    FileUrl = $"{baseUrl}/uploads/files/{fileName}",
                    Slug = slug
                };

                _context.Libraries.Add(library);
                await _context.SaveChangesAsync();

                return Ok(library);
            }
            catch
            {
                if (System.IO.File.Exists(coverPath))
                    System.IO.File.Delete(coverPath);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                return StatusCode(500, new { message = "Gagal menyimpan data" });
            }
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] LibraryRequest request)
        {
            var existing = await _context.Libraries.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Data tidak ditemukan" });

            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var allowedImageTypes = new[] { "image/jpeg", "image/png", "image/jpg" };

            if (request.Cover != null)
            {
                if (request.Cover.Length > 2 * 1024 * 1024)
                    return BadRequest(new { message = "Ukuran cover maksimal 2MB" });

                if (!allowedImageTypes.Contains(request.Cover.ContentType))
                    return BadRequest(new { message = "Cover harus JPG/PNG" });
            }

            if (request.File != null)
            {
                if (request.File.Length > 10 * 1024 * 1024)
                    return BadRequest(new { message = "Ukuran PDF maksimal 10MB" });

                if (request.File.ContentType != "application/pdf")
                    return BadRequest(new { message = "File harus PDF" });
            }

            // UPDATE COVER
            if (request.Cover != null)
            {
                var oldPath = existing.CoverUrl?.Replace(baseUrl, "wwwroot");

                var coverName = Guid.NewGuid() + Path.GetExtension(request.Cover.FileName);
                var newPath = Path.Combine("wwwroot/uploads/covers", coverName);

                using var stream = new FileStream(newPath, FileMode.Create);
                await request.Cover.CopyToAsync(stream);

                existing.CoverUrl = $"{baseUrl}/uploads/covers/{coverName}";

                if (!string.IsNullOrEmpty(oldPath) && System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            // UPDATE FILE
            if (request.File != null)
            {
                var oldPath = existing.FileUrl?.Replace(baseUrl, "wwwroot");

                var fileName = Guid.NewGuid() + ".pdf";
                var newPath = Path.Combine("wwwroot/uploads/files", fileName);

                using var stream = new FileStream(newPath, FileMode.Create);
                await request.File.CopyToAsync(stream);

                existing.FileUrl = $"{baseUrl}/uploads/files/{fileName}";

                if (!string.IsNullOrEmpty(oldPath) && System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            existing.Title = request.Title;
            existing.Author = request.Author;
            existing.Description = request.Description;
            existing.Category = request.Category;
            existing.Isbn = request.Isbn;
            existing.ReleaseDate = request.ReleaseDate;
            existing.Slug = GenerateSlug(request.Title);

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Libraries.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = "Data tidak ditemukan" });

            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var coverPath = existing.CoverUrl?.Replace(baseUrl, "wwwroot");
            var filePath = existing.FileUrl?.Replace(baseUrl, "wwwroot");

            if (!string.IsNullOrEmpty(coverPath) && System.IO.File.Exists(coverPath))
                System.IO.File.Delete(coverPath);

            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _context.Libraries.Remove(existing);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data berhasil dihapus" });
        }

        // =========================
        // HELPER: GENERATE SLUG
        // =========================
        private string GenerateSlug(string title)
        {
            return title.ToLower()
                .Trim()
                .Replace(" ", "-")
                .Replace(".", "")
                .Replace(",", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace("&", "dan")
                .Replace("/", "")
                .Replace("\\", "");
        }
    }
}