using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class RoleRepoImpl : IRoleRepo
    {
        private readonly ApplicationDbContext _ctx;
        public RoleRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Role>> GetAllAsync()
            => await _ctx.Roles.ToListAsync();

        public async Task<Role?> GetByIdAsync(long id)
            => await _ctx.Roles.FindAsync(id);

        public async Task<Role> CreateAsync(Role e)
        {
            await _ctx.Roles.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<Role> UpdateAsync(Role e)
        {
            _ctx.Roles.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task DeleteAsync(Role e)
        {
            _ctx.Roles.Remove(e);
            await _ctx.SaveChangesAsync();
        }

        public Task SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}