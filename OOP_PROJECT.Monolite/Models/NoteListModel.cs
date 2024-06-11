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

        public virtual ICollection<NoteModel> Notes { get; set; } = new List<NoteModel>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        [Required]
        public virtual UserModel User { get; set; }

        public Guid FolderId { get; set; }
        [Required]
        public virtual FolderModel Folder { get; set; }
    }
}
