using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;


namespace QLDT.Repository.impl
{
    public class RoleRepoImpl : RoleRepo
    {
        private readonly ApplicationDbContext _context;
        public RoleRepoImpl(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Role>> GetAllAsync()
           => await _context.Roles.ToListAsync();
    }
}
