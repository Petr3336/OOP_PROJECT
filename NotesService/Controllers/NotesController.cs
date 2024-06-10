using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;
using System.Security.Claims;
namespace NotesService.Controllers
{
    
    [ApiController]
    [Route("api/notesservice/[controller]")]
    public class NotesController : ControllerBase
    {

        private readonly NotesContext _context;

        public NotesController(NotesContext context)
        {
            _context = context;
        }


        // GET: api/notes
        //[Authorize]
        [HttpGet("getnotes")]
        public async Task<ActionResult<IEnumerable<NoteModel>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/notes/5
        //[Authorize]
        [HttpGet("getnotebyid/{id}")]
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
       // [Authorize]
        [HttpPost("createnote")]
        public async Task<ActionResult<NoteModel>> PostNote(NoteModel note)
        {//фейк
            //if (note == null)
            //{
            //    return BadRequest(new { message = "Note cannot be null" });
            //}

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //// Создание новой папки
            //var folder = new FolderModel();
            //_context.Folders.Add(folder);
            //await _context.SaveChangesAsync();

            //// Привязка заметки к папке
            //note.FolderId = folder.Id;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        // PUT: api/notes/5
        [HttpPut("updatenotebyid/{id}")]
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
        [HttpDelete("deletenotebyid/{id}")]
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
