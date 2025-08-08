using QLDT.Data;
using QLDT.Models;
using Microsoft.EntityFrameworkCore;

namespace QLDT.Repository.impl
{
    public class MajorRepoImpl : MajorRepo
    {
        private readonly ApplicationDbContext _ctx;
        public MajorRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Major>> GetAllAsync()
            => await _ctx.Majors
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();

        public async Task<IEnumerable<Major>> GetAllIsActiveAsync()
            => await _ctx.Majors
            .Where(x => x.IsActive == true)
            .OrderBy(c => c.Name)
            .ToListAsync();


        public async Task<Major> CreateAsync(Major e)
        {
            await _ctx.Majors.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<Major?> GetByIdAsync(long id)
            => await _ctx.Majors.FindAsync(id);

        public async Task<Major> UpdateAsync(Major e)
        {
            _ctx.Majors.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }
    }
}
