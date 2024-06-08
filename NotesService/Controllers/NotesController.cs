using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;
namespace NotesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext _context;

        public NotesController(NotesContext context)
        {
            _context = context;
        }


        // GET: api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteModel>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteModel>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<NoteModel>> PostNote(NoteModel note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        // PUT: api/notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, [FromBody] NoteModel note)
        {
            var existingNote = await _context.Notes.FindAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }
            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            existingNote.Category = note.Category;
            existingNote.MediaUrl = note.MediaUrl;
            existingNote.UpdatedAt = DateTime.Now;



            _context.Entry(existingNote).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(note.Id))
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

        // DELETE: api/notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
