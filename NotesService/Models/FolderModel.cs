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
        // Навигационное свойство для родительской папки
        [ForeignKey("ParentFolderId")]
        public virtual FolderModel? ParentFolder { get; set; }
        // Коллекция подпапок
        public virtual ICollection<Guid>? SubFolders { get; set; }
        // Коллекция списков заметок в папке
        public virtual ICollection<Guid>? NoteLists { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Навигационное свойство для пользователя
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}