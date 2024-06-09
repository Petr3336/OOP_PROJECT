using System.ComponentModel.DataAnnotations;

namespace NotesService.Models
{
    public class NoteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }

        public string? MediaUrl { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
     //фейк   public int FolderId { get; set; } // Новое свойство
     //фейк   public FolderModel Folder { get; set; } = null!; // Новое свойство
    }
}
