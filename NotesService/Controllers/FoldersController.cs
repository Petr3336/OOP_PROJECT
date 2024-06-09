using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;

namespace NotesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoldersController : ControllerBase
    {
        private readonly NotesContext _context;
        public FoldersController(NotesContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FolderModel>>> GetFolder()
        {
            return await _context.Folders.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<FolderModel>> PostFolder(FolderModel folder/*, NoteModel noteModel*/)
        {
            //_context.Notes.Add(noteModel);
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFolder), new { id = folder.Id }, folder);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FolderModel>> GetFolders(int id)
        {
            var foler = await _context.Folders.FindAsync(id);

            if (foler == null)
            {
                return NotFound();
            }

            return foler;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolder(int id, [FromBody] FolderModel folder)
        {
            var updatedFolder = await _context.Folders.FindAsync(id);

            if (updatedFolder == null)
            {
                return NotFound();
            }
            updatedFolder.FolderName = folder.FolderName;
            updatedFolder.FolderIds = folder.FolderIds;
            updatedFolder.NoteIds = folder.NoteIds;
            updatedFolder.UpdatedAt = DateTime.Now;
            updatedFolder.HasSmthngInIt=folder.HasSmthngInIt;



            _context.Entry(updatedFolder).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!FolderExists(folder.Id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool FolderExists(int id)
        {
            return _context.Folders.Any(f => f.Id == id);
        }
    }

}

