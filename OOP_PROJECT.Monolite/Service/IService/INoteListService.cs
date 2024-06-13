using NotesService.Models;
using NotesService.Models.RequestModels;

namespace OOP_PROJECT.Monolite.Service.IService
{
    public interface INoteListService
    {
        Task<IEnumerable<NoteListModel>> GetNoteListsAsync(string userId);
        Task<NoteListModel> GetNoteListAsync(int id, string userId);
        Task<NoteListModel> CreateNoteListAsync(CreateOrModifyNoteListRequest noteListRequest, string userId);
        Task<bool> UpdateNoteListAsync(int id, CreateOrModifyNoteListRequest noteListRequest, string userId);
        Task<bool> DeleteNoteListAsync(int id, string userId);
        Task<bool> NoteListExistsAsync(int id);
    }
}
