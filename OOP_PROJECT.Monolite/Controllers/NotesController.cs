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
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/NoteLists
        [HttpGet("getallnotesfromnotelistbyid/{id}")]
        public async Task<ActionResult<NoteModel>> GetNoteFromNoteList(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list =  await _context.NoteLists.FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);
            var listid = list.Id;
            var notes = await _context.Notes
                .Where(n => n.UserId == userId &&  n.NoteListId==listid)
                .ToListAsync();
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        // GET: api/NoteLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteModel>> GetNote(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notes = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        // POST: api/NoteLists
        [HttpPost]
        public async Task<ActionResult<NoteModel>> PostNoteList(CreateOrModifyNoteRequest notes)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var note = new NoteModel()
            {
                Name = notes.Name,
                Description = notes.Description,
                Content = notes.Content,
                UserId = userId,
                NoteListId = notes.NoteListId,
                Completed = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // PUT: api/NoteLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteList(int id, CreateOrModifyNoteRequest noteupdt)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noteupdate = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
            if (noteupdate == null)
            {
                return BadRequest();
            }

            noteupdate.UpdatedAt = DateTime.UtcNow;
            noteupdate.Name = noteupdt.Name;
            noteupdate.Description = noteupdt.Description;
            noteupdate.Content = noteupdt.Content;
            noteupdate.Completed = false;
            _context.Entry(noteupdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNote", new { id = noteupdate.Id }, noteupdate);
        }
        [HttpPut("updateCheckbox/{id}")]
        public async Task<IActionResult> PutNoteList(int id, UpdateNoteCompletedRequest noteupdt)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noteupdate = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);
            if (noteupdate == null)
            {
                return BadRequest();
            }

            noteupdate.Completed = noteupdt.Completed;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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
        public async Task<IActionResult> DeleteNote(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var noteList = await _context.Notes.FirstOrDefaultAsync(nl => nl.Id == id && nl.UserId == userId);
            if (noteList == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(noteList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}