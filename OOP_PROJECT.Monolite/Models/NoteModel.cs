using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotesService.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Content { get; set; }

        public bool Completed { get; set; }

        [Required]
        public int NoteListId { get; set; }

        [JsonIgnore]
        public NoteListModel NoteList { get; set; }

        [Required]
        public string UserId { get; set; }

        public UserModel User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}