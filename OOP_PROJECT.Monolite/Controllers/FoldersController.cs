using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesService.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using NotesService.Models.RequestModels;

[Route("api/folders")]
[ApiController]
[Authorize]
public class FoldersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserModel> _userManager;

    public FoldersController(ApplicationDbContext context, UserManager<UserModel> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: api/folders
    [HttpGet]
    public async Task<ActionResult<FolderModel>> GetRootFolder()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var rootFolder = await _context.Folders.FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId) && f.ParentFolderId == null);

        if (rootFolder == null)
        {
            return NotFound();
        }

        return rootFolder;
    }

    // GET: api/folders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<FolderModel>> GetFolder(Guid id)
    {
        var folder = await _context.Folders.FindAsync(id);

        if (folder == null)
        {
            return NotFound();
        }

        return folder;
    }

    // POST: api/folders
    [HttpPost]
    public async Task<ActionResult<FolderModel>> CreateFolder(CreateOrModifyFolderRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);

        // Проверяем, существует ли корневая папка у пользователя
        if (!user.RootFolderId.HasValue)
        {
            // Если нет, создаем корневую папку
            var rootFolder = new FolderModel
            {
                Name = "Root",
                UserId = Guid.Parse(userId),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Folders.Add(rootFolder);
            await _context.SaveChangesAsync();

            // Обновляем RootFolderId пользователя
            user.RootFolderId = rootFolder.Id;
            await _userManager.UpdateAsync(user);
        }

        // Создаем новую папку с ParentFolderId, равным RootFolderId пользователя
        var folder = new FolderModel
        {
            Name = request.Name,
            Description = request.Description,
            ParentFolderId = user.Id, // Устанавливаем ParentFolderId
            UserId = Guid.Parse(userId),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Folders.Add(folder);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFolder), new { id = folder.Id }, folder);
    }

    // PUT: api/folders/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFolder(Guid id, CreateOrModifyFolderRequest request)
    {
        var folder = await _context.Folders.FindAsync(id);
        if (folder == null)
        {
            return NotFound();
        }

        folder.Name = request.Name;
        folder.Description = request.Description;
        folder.UpdatedAt = DateTime.UtcNow;

        _context.Entry(folder).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/folders/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFolder(Guid id)
    {
        var folder = await _context.Folders.FindAsync(id);
        if (folder == null)
        {
            return NotFound();
        }

        _context.Folders.Remove(folder);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
