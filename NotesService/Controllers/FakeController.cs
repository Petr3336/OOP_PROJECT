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
    public class FakeController : ControllerBase
    {
        private readonly INoteService _noteService;

        public FakeController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolderWithNotes(int id)
        {
            var folder = await _noteService.GetFolderWithNotesAsync(id);
            if (folder == null)
            {
                return NotFound();
            }
            return Ok(folder);
        }

        [HttpPost("{folderId}/notes/{noteId}")]
        public async Task<IActionResult> AddNoteToFolder(int folderId, int noteId)
        {
            await _noteService.ConnectFolderToNoteAsync(folderId, noteId);
            return NoContent();
        }

        [HttpDelete("{folderId}/notes/{noteId}")]
        public async Task<IActionResult> RemoveNoteFromFolder(int folderId, int noteId)
        {
            await _noteService.DisconnectFolderFromNoteAsync(folderId, noteId);
            return NoContent();
        }
    }
}
