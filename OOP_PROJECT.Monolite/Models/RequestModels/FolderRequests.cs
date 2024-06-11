using System.ComponentModel.DataAnnotations;

namespace NotesService.Models.RequestModels
{
    public class CreateOrModifyFolderRequest
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; } // Необязательное поле
    }
}
