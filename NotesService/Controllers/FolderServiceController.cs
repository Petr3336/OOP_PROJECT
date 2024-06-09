using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesService.Services.Interface;
using NotesService.Services.Class;
using NotesService.Data;
using NotesService.Models;

namespace NotesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderServiceController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderServiceController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolderWithNotes(int id)
        {
            var folder = await _folderService.GetFolderWithNotesAsync(id);
            if (folder == null)
            {
                return NotFound();
            }
            return Ok(folder);
        }

        [HttpPost("{folderId}/notes")]
        public async Task<IActionResult> AddNoteToFolder(int folderId, [FromBody] NoteModel note)
        {
            await _folderService.AddNoteToFolderAsync(folderId, note);
            return NoContent();
        }

        [HttpDelete("{folderId}/notes/{noteId}")]
        public async Task<IActionResult> RemoveNoteFromFolder(int folderId, int noteId)
        {
            await _folderService.RemoveNoteFromFolderAsync(folderId, noteId);
            return NoContent();
        }
    }
}
