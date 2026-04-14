using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SttbApi.Data;
using SttbApi.Models;

namespace SttbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MediaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? type)
        {
            var query = _context.Media.AsQueryable();

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(m => m.Type == type);
            }

            var data = await query
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    m.Slug,
                    m.Type,
                    m.CoverImage,
                    m.PublishedAt,
                    Category = m.Category.Name
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var data = await _context.Media
                .Where(m => m.Slug == slug)
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    m.Slug,
                    m.Type,
                    m.CoverImage,
                    m.PublishedAt,

                    Category = m.Category.Name,

                    Article = m.Article == null ? null : new
                    {
                        m.Article.Author,
                        m.Article.AuthorImage,
                        m.Article.AuthorDescription,
                        m.Article.Tagline,
                        m.Article.Content
                    },

                    Video = m.Video == null ? null : new
                    {
                        m.Video.YoutubeUrl,
                        m.Video.Theme,
                        m.Video.Description
                    },

                    Journal = m.Journal == null ? null : new
                    {
                        m.Journal.FileUrl
                    },

                    Bulletin = m.Bulletin == null ? null : new
                    {
                        m.Bulletin.Author,
                        m.Bulletin.PdfUrl,
                        m.Bulletin.TitleDescription,
                        m.Bulletin.Description
                    },

                    Monograph = m.Monograph == null ? null : new
                    {
                        m.Monograph.Author,
                        m.Monograph.Image,
                        m.Monograph.DescriptionTitle,
                        m.Monograph.Writer,
                        m.Monograph.Synopsis,
                        m.Monograph.ISBN
                    }
                })
                .FirstOrDefaultAsync();

            if (data == null) return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MediaRequest req)
        {
            var slug = req.Title.ToLower().Replace(" ", "-");

            var media = new Media
            {
                Title = req.Title,
                Type = req.Type,
                CategoryId = req.CategoryId,
                CoverImage = req.CoverImage,
                PublishedAt = req.PublishedAt,
                Slug = slug
            };

            _context.Media.Add(media);
            await _context.SaveChangesAsync();

            switch (req.Type)
            {
                case "article":
                    _context.Article.Add(new Article
                    {
                        MediaId = media.Id,
                        Author = req.Author,
                        AuthorImage = req.AuthorImage,
                        AuthorDescription = req.AuthorDescription,
                        Tagline = req.Tagline,
                        Content = req.Content
                    });
                    break;

                case "video":
                    _context.Video.Add(new Video
                    {
                        MediaId = media.Id,
                        YoutubeUrl = req.YoutubeUrl,
                        Theme = req.VideoTheme,
                        Description = req.VideoDescription
                    });
                    break;

                case "journal":
                    _context.Journal.Add(new Journal
                    {
                        MediaId = media.Id,
                        FileUrl = req.FileUrl
                    });
                    break;

                case "bulletin":
                    _context.Bulletin.Add(new Bulletin
                    {
                        MediaId = media.Id,
                        Author = req.Author,
                        PdfUrl = req.FileUrl,
                        TitleDescription = req.TitleDescription,
                        Description = req.Description
                    });
                    break;

                case "monograph":
                    _context.Monograph.Add(new Monograph
                    {
                        MediaId = media.Id,
                        Author = req.Author,
                        Image = req.Image,
                        DescriptionTitle = req.DescriptionTitle,
                        Writer = req.Writer,
                        Synopsis = req.Synopsis,
                        ISBN = req.ISBN
                    });
                    break;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MediaRequest req)
        {
            var media = await _context.Media
                .Include(m => m.Article)
                .Include(m => m.Video)
                .Include(m => m.Journal)
                .Include(m => m.Bulletin)
                .Include(m => m.Monograph)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (media == null) return NotFound();

            //  Update base Media
            media.Title = req.Title;
            media.CategoryId = req.CategoryId;
            media.CoverImage = req.CoverImage;
            media.PublishedAt = req.PublishedAt;
            media.Slug = req.Title.ToLower().Replace(" ", "-");

            //  HANDLE PER TYPE
            switch (media.Type)
            {
                case "article":
                    if (media.Article != null)
                    {
                        media.Article.Author = req.Author;
                        media.Article.AuthorImage = req.AuthorImage;
                        media.Article.AuthorDescription = req.AuthorDescription;
                        media.Article.Tagline = req.Tagline;
                        media.Article.Content = req.Content;
                    }
                    break;

                case "video":
                    if (media.Video != null)
                    {
                        media.Video.YoutubeUrl = req.YoutubeUrl;
                        media.Video.Theme = req.VideoTheme;
                        media.Video.Description = req.VideoDescription;
                    }
                    break;

                case "journal":
                    if (media.Journal != null)
                    {
                        media.Journal.FileUrl = req.FileUrl;
                    }
                    break;

                case "bulletin":
                    if (media.Bulletin != null)
                    {
                        media.Bulletin.Author = req.Author;
                        media.Bulletin.PdfUrl = req.FileUrl;
                        media.Bulletin.TitleDescription = req.TitleDescription;
                        media.Bulletin.Description = req.Description;
                    }
                    break;

                case "monograph":
                    if (media.Monograph != null)
                    {
                        media.Monograph.Author = req.Author;
                        media.Monograph.Image = req.Image;
                        media.Monograph.DescriptionTitle = req.DescriptionTitle;
                        media.Monograph.Writer = req.Writer;
                        media.Monograph.Synopsis = req.Synopsis;
                        media.Monograph.ISBN = req.ISBN;
                    }
                    break;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var media = await _context.Media.FindAsync(id);
            if (media == null) return NotFound();

            _context.Media.Remove(media);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted" });
        }


    }
}
