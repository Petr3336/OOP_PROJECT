using System.ComponentModel.DataAnnotations;

namespace NotesService.Models.RequestModels
{
    public class CreateOrModifyNoteRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }
        [Required]
        public int NoteListId { get; set; }
    }
}