using System.ComponentModel.DataAnnotations;

namespace NotesService.Models
{///<summary>
    ///folders
    ///</summary>
    public class FolderModel
    {
        public int Id { get; set; }
        public string FolderName { get; set; } = "NewFolder";
        public List<int>? FolderIds { get; set; }
        public List<int>? NoteIds { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public bool HasSmthngInIt { get; set; } = false;
        public ICollection<NoteModel> Notes { get; set; } = new List<NoteModel>();
        public NoteListModel NoteList { get; set; }
    }
}