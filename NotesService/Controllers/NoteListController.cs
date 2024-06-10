using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;

namespace NotesService.Controllers
{
    [ApiController]
    [Route("api/notesservice/[controller]")]
    public class NoteListController : ControllerBase
    {
        private readonly NotesContext _context;
        public NoteListController(NotesContext context)
        {
            _context = context;
        }
        [HttpGet("getnotelists")]
        public async Task<ActionResult<IEnumerable<NoteListModel>>> GetList()
        {
            return await _context.NoteLists.ToListAsync();
        }
        [HttpPost("createnotelist")]
        public async Task<ActionResult<NoteListModel>> PostList(NoteListModel list/*, NoteModel noteModel*/)
        {
            //_context.Notes.Add(noteModel);
            _context.NoteLists.Add(list);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetList), new { id = list.Id }, list);
        }
        [HttpGet("getnotelistbyid/{id}")]
        public async Task<ActionResult<NoteListModel>> GetList(int id)
        {
            var list = await _context.NoteLists.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }
        [HttpPut("updatenotelistbyid/{id}")]
        public async Task<IActionResult> PutFolder(int id, [FromBody] NoteListModel list)
        {
            var updatedList = await _context.NoteLists.FindAsync(id);

            if (updatedList == null)
            {
                return NotFound();
            }
            

            updatedList.Id=list.Id;
            updatedList.FolderIdls=list.FolderIdls;
            updatedList.IsInFolder = list.IsInFolder;
            updatedList.HasSmtngInIt= list.HasSmtngInIt;
            updatedList.Notes = list.Notes;

            _context.Entry(updatedList).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(list.Id))
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
        [HttpDelete("deletenotelistbyid/{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var folder = await _context.NoteLists.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            _context.NoteLists.Remove(folder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ListExists(int id)
        {
            return _context.NoteLists.Any(f => f.Id == id);
        }
    }
}
