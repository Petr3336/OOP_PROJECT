using NotesService.Models;
namespace NotesService.Services.Interface
{
    public interface IFolderService
    {
        Task<FolderModel> GetFolderWithNotesAsync(int folderId);
        Task AddNoteToFolderAsync(int folderId, NoteModel note);
        Task RemoveNoteFromFolderAsync(int folderId, int noteId);
    }
}
