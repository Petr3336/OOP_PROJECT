using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;
using System.Security.Claims;
namespace NotesService.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext _context;
        private readonly UserManager<UserModel> _userManager;

        public NotesController(NotesContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<NoteModel>> CreateNote(NoteModel note)
        {
            var user = await _userManager.GetUserAsync(User);
            note.User = user;
            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = DateTime.UtcNow;

            // Проверка наличия списка заметок
            var noteList = await _context.NoteLists.FindAsync(note.NoteList);
            if (noteList == null || noteList.User != user)
            {
                return BadRequest("NoteList not found or not owned by the user.");
            }

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, NoteModel note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (note.User != user)
            {
                return Unauthorized();
            }

            note.UpdatedAt = DateTime.UtcNow;

            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
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
    }
}
