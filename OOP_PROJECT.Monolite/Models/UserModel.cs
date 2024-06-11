using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesService.Models
{
    //Модель пользователя
    public class UserModel : IdentityUser<Guid>
    {
        public Guid? RootFolderId { get; set; }
        public virtual FolderModel? RootFolder { get; set; }
    }
}
