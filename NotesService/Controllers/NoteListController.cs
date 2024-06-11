using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;

namespace NotesService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteListsController : ControllerBase
    {
        private readonly NotesContext _context;
        private readonly UserManager<UserModel> _userManager;

        public NoteListsController(NotesContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/NoteLists
        [HttpPost]
        public async Task<ActionResult<NoteListModel>> CreateNoteList(NoteListModel noteList)
        {
            var user = await _userManager.GetUserAsync(User);
            noteList.User = user;
            noteList.CreatedAt = DateTime.UtcNow;
            noteList.UpdatedAt = DateTime.UtcNow;

            // Проверка наличия родительской папки
            var folder = await _context.Folders.FindAsync(noteList.Folder);
            if (folder == null || folder.User != user)
            {
                return BadRequest("Folder not found or not owned by the user.");
            }

            _context.NoteLists.Add(noteList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteList", new { id = noteList.Id }, noteList);
        }

        // PUT: api/NoteLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteList(Guid id, NoteListModel noteList)
        {
            if (id != noteList.Id)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            if (noteList.User != user)
            {
                return Unauthorized();
            }

            noteList.UpdatedAt = DateTime.UtcNow;

            _context.Entry(noteList).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/NoteLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteList(Guid id)
        {
            var noteList = await _context.NoteLists.FindAsync(id);
            if (noteList == null)
            {
                return NotFound();
            }

            _context.NoteLists.Remove(noteList);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
