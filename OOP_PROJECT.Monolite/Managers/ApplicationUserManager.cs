using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NotesService.Models;

namespace OOP_PROJECT.Monolite.Managers
{
    public class ApplicationUserManager : UserManager<UserModel>
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserManager(
            IUserStore<UserModel> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserModel> passwordHasher,
            IEnumerable<IUserValidator<UserModel>> userValidators,
            IEnumerable<IPasswordValidator<UserModel>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<UserModel>> logger,
            ApplicationDbContext context) : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {
            _context = context;
        }

        public override async Task<IdentityResult> CreateAsync(UserModel user, string password)
        {
            // Сначала создаем пользователя
            var result = await base.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Если пользователь успешно создан, создаем для него корневую папку
                var rootFolder = new FolderModel
                {
                    Name = "InitialRootFolder",
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };



                _context.Folders.Add(rootFolder);
                await _context.SaveChangesAsync();

                // Обновляем пользователя с информацией о корневой папке
                user.RootFolderId = rootFolder.Id;
                user.RootFolder = rootFolder;
                
            }

            return result;
        }
    }

}
