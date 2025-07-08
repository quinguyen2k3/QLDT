using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class EducationLevelRepoImpl : EducationLevelRepo
    {
        private readonly ApplicationDbContext _ctx;
        public EducationLevelRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<EducationLevel>> GetAllAsync()
            => await _ctx.EducationLevels.ToListAsync();

        public async Task<EducationLevel?> GetByIdAsync(long id)
            => await _ctx.EducationLevels.FindAsync(id);

        public async Task<EducationLevel> CreateAsync(EducationLevel e)
        {
            await _ctx.EducationLevels.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<EducationLevel> UpdateAsync(EducationLevel e)
        {
            _ctx.EducationLevels.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

      

        public Task SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
