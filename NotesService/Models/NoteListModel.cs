using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    // Модель списка заметок
    public class NoteListModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Guid> Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
        [Required]
        [ForeignKey("FolderId")]
        public FolderModel Folder { get; set; }
    }
}
