using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    //Модель пользователя
    public class UserModel
    {
        public int Id { get; set; }
        public FolderModel? RootFolder { get; set; }
    }
}
