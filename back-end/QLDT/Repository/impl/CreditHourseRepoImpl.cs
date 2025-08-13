using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class CreditHourseRepoImpl : CreditHourseRepo
    {
        private readonly ApplicationDbContext _ctx;
        public CreditHourseRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<CreditHourse>> GetAllAsync()
            => await _ctx.CreditHourses
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();

        public async Task<IEnumerable<CreditHourse>> GetAllIsActiveAsync()
            => await _ctx.CreditHourses
            .Where(x => x.IsActive == true)
            .OrderBy(c => c.Hour)
            .ToListAsync();

        public async Task<CreditHourse> CreateAsync(CreditHourse e)
        {
            await _ctx.CreditHourses.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<CreditHourse?> GetByIdAsync(long id)
            => await _ctx.CreditHourses.FindAsync(id);

        public async Task<CreditHourse> UpdateAsync(CreditHourse e)
        {
            _ctx.CreditHourses.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }
    }
}
