using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class PartRepoImpl : IPartRepo
    {
        private readonly ApplicationDbContext _ctx;
        public PartRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Part>> GetAllAsync() =>
            await _ctx.Parts.ToListAsync();

        public async Task<Part?> GetByIdAsync(long id) =>
            await _ctx.Parts.FindAsync(id);

        public async Task<Part> CreateAsync(Part entity)
        {
            await _ctx.Parts.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<Part> UpdateAsync(Part entity)
        {
            _ctx.Parts.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Part entity)
        {
            _ctx.Parts.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public Task SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
