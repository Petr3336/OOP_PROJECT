using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    public class NoteListModel
    {
        public int Id { get; set; }
        public bool HasSmtngInIt { get; set; }
        public bool IsInFolder  { get; set; }
        public List<NoteModel> Notes { get; set; } = new List<NoteModel>();
        [ForeignKey("FolderIdls")]
        public int FolderIdls { get; set; }
    }
}
