using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NotesService.Models
{
    public class NoteListModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Position { get; set; }

        [Required]
        public string UserId { get; set; }

        [JsonIgnore]
        public UserModel User { get; set; }

        [JsonIgnore]
        public ICollection<NoteModel> Notes { get; set; } = new List<NoteModel>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}