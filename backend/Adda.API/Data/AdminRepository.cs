using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adda.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Adda.API.Data;

public class AdminRepository(DataContext context) : IAdminRepository
{
    private readonly DataContext _context = context;

    public async Task<IEnumerable<object>> GetAllPhotosAsync() => await _context.Photos
            .IgnoreQueryFilters()
            .Where(p => !p.IsApproved)
            .Select(
                u =>
                    new
                    {
                        Id = u.Id,
                        UserName = u.User.UserName,
                        Url = u.Url,
                        IsApproved = u.IsApproved
                    }
            )
            .ToListAsync();

    public async Task<IEnumerable<object>> GetUsersWithRolesAsync() => await _context.Users
            .OrderBy(x => x.UserName)
            .Select(
                user =>
                    new
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Roles = (
                            from userRole in user.UserRoles
                            join role in _context.Roles on userRole.RoleId equals role.Id
                            select role.Name
                        ).ToList()
                    }
            )
            .ToListAsync();

    public async Task<Photo> GetPhotoAsync(int photoId) => await _context.Photos
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == photoId);

    public async Task<bool> SaveAllAsync() => await _context.SaveChangesAsync() > 0;

    public void Delete<T>(T entity) where T : class => _context.Remove(entity);
}