using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    // Модель папки
    public class FolderModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid? ParentFolderId { get; set; }
        public virtual ICollection<FolderModel>? SubFolders { get; set; } = new List<FolderModel>();
        public virtual ICollection<NoteListModel>? NoteLists { get; set; } = new List<NoteListModel>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid? UserId { get; set; }
    }
}