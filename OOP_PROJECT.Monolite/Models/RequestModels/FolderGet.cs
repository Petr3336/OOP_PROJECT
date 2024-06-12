using System.ComponentModel.DataAnnotations;

namespace NotesService.Models.RequestModels
{
    public class GetFromFolder
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<NoteListModel> NoteListModels { get; set; }
    }
}