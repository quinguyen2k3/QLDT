using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class PermissionRepoImpl : PermissionRepo
    {
        private readonly ApplicationDbContext _context;
        public PermissionRepoImpl(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<Permission>> GetAllByRolenameAsync(string name)
        {
            return  await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .Where(rp => rp.Role != null && rp.Role.Name == name && rp.Permission != null)
                .Select(rp => rp.Permission)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetAllByUserIdAsync(long id)
        {
            return await _context.Permissions
                .Where(p => p.RolePermissions.Any(rp => _context.Users.Any(u => u.Id == id && u.RoleId == rp.RoleId)))
                .Distinct()
                .ToListAsync();
        }
    }
}
