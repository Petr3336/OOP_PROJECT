using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    // Модель заметки
    public class NoteModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid UserId { get; set; }
        [Required]
        public virtual UserModel User { get; set; }

        public Guid NoteListId { get; set; }
        [Required]
        public virtual NoteListModel NoteList { get; set; }
    }
}
