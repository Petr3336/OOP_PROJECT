using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Models;
using System.Security.Claims;
using NotesService.Models.RequestModels;
namespace NotesService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NoteListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NoteListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NoteLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteListModel>>> GetNoteLists()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noteLists = await _context.NoteLists
                .Where(nl => nl.UserId == userId)
                .ToListAsync();
            return Ok(noteLists);
        }

        // GET: api/NoteLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteListModel>> GetNoteList(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noteList = await _context.NoteLists
                .Include(nl => nl.Notes) // Загружаем заметки для списка
                .FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);

            if (noteList == null)
            {
                return NotFound();
            }

            return Ok(noteList);
        }

        // POST: api/NoteLists
        [HttpPost]
        public async Task<ActionResult<NoteListModel>> PostNoteList(CreateOrModifyNoteListRequest noteList)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var newlist = new NoteListModel()
            {
                Name = noteList.Name,
                Description = noteList.Description,
                Position = noteList.Position,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.NoteLists.Add(newlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteList", new { id = newlist.Id }, noteList);
        }

        // PUT: api/NoteLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteList(int id, CreateOrModifyNoteListRequest noteList)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notelistupdate = await _context.NoteLists
                .Include(nl => nl.Notes) // Загружаем заметки для списка
                .FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);
            if (notelistupdate == null)
            {
                return BadRequest();
            }

            notelistupdate.UpdatedAt = DateTime.UtcNow;
            notelistupdate.Name = noteList.Name;
            notelistupdate.Description = noteList.Description;
            notelistupdate.Position = noteList.Position;
            _context.Entry(notelistupdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/NoteLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteList(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noteList = await _context.NoteLists.FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);
            if (noteList == null)
            {
                return NotFound();
            }

            _context.NoteLists.Remove(noteList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteListExists(int id)
        {
            return _context.NoteLists.Any(e => e.Id == id);
        }
    }
}