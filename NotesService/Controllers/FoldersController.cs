using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesService.Data;
using NotesService.Models;
using NotesService.Models.RequestModels;

namespace NotesService.Controllers
{
    [Authorize]
    [Route("api/noteservice/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly NotesContext _context;
        private readonly UserManager<UserModel> _userManager;

        public FoldersController(NotesContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: api/Folders
        [HttpPost]
        public async Task<ActionResult<FolderModel>> CreateFolder([FromBody] CreateorModifyFolderRequest request)
        {
            Console.WriteLine(User);
            var user = HttpContext.Items["UserModel"] as UserModel; // Явное приведение типа
            if (user == null)
            {
                // Обработка случая, когда пользователь не найден
                return Unauthorized();
            }

            var folder = new FolderModel
            {
                Name = request.Name,
                Description = request.Description,
                User = user, // Используйте UserId, так как User ожидает объект UserModel
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
                // Остальные поля инициализируются здесь или остаются пустыми
            };

            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolder", new { id = folder.Id }, folder);
        }

        // DELETE: api/Folders/5
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

        // PUT: api/Folders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFolder(Guid id, [FromBody] CreateorModifyFolderRequest request)
        {
            var folder = await _context.Folders.FindAsync(id);
            if (folder == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (folder.User != user)
            {
                return Unauthorized();
            }

            folder.Name = request.Name;
            folder.Description = request.Description ?? folder.Description; // Если Description не предоставлен, оставляем текущее значение
            folder.UpdatedAt = DateTime.UtcNow;

            _context.Entry(folder).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}

