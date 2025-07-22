using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class PartRepoImpl : PartRepo
    {
        private readonly ApplicationDbContext _ctx;
        public PartRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Part>> GetAllAsync()
            => await _ctx.Parts.ToListAsync();
        public async Task<IEnumerable<Part>> GetAllIsActiveAsync()
            => await _ctx.Parts
            .Where(x => x.IsActive == true)
            .ToListAsync();

        public async Task<IEnumerable<Part>> GetAllByUsernameAsync(string username)
            => await _ctx.Parts
            .Where(p => p.CreatedBy == username)
            .ToListAsync();

        public async Task<Part> CreateAsync(Part e)
        {
            await _ctx.Parts.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<Part?> GetByIdAsync(long id)
            => await _ctx.Parts.FindAsync(id);

        public async Task<Part> UpdateAsync(Part e)
        {
            _ctx.Parts.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }
    }
}