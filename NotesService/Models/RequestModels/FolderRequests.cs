using System.ComponentModel.DataAnnotations;

namespace NotesService.Models.RequestModels
{
    public class CreateorModifyFolderRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; } // Необязательное поле
    }
}
