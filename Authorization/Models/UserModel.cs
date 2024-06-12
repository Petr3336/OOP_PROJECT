using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authorization.Models
{
    public class UserModel : IdentityUser<Guid>
    {
    }
}
