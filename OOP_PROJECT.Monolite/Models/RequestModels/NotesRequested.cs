using System.ComponentModel.DataAnnotations;

namespace NotesService.Models.RequestModels
{
    public class CreateOrModifyNoteRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Content { get; set; }

        public int NoteListId { get; set; }
    }
}