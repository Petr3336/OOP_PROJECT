using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NotesService.Models
{
    public class FolderModel
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
        public ICollection<NoteListModel> NoteLists { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}