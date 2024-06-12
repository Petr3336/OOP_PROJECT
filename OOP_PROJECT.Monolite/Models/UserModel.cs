using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace NotesService.Models
{
    public class UserModel : IdentityUser
    {


        [JsonIgnore]
        public ICollection<NoteListModel> NoteLists { get; set; } = new List<NoteListModel>();


    }
}