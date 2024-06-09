using NotesService.Models;
namespace NotesService.Services.Interface
{
    public interface INoteService
    {
        Task<FolderModel> GetFolderWithNotesAsync(int folderId);
        Task ConnectFolderToNoteAsync(int folderId, int noteId);
        Task DisconnectFolderFromNoteAsync(int folderId, int noteId);
    }
}
