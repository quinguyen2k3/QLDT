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
            return await _context.RolePermissions
                .Where(rp => rp.Role.Name == name)
                .Select(rp => rp.Permission)
                .Distinct()
                .ToListAsync();
        }
    }
}
