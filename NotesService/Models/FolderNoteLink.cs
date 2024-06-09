using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NotesService.Models
{
    public class FolderNoteLink
    {
        [Key]
        public int NoteId { get; set; }
        public int FolderId { get; set; }
        [ForeignKey("NoteId")]
        public virtual NoteModel Note { get; set; }

        [ForeignKey("FolderId")]
        public virtual FolderModel Folder { get; set; }
    }
}
