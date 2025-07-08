using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class TrainingUnitRepoImpl : TrainingUnitRepo
    {
        private readonly ApplicationDbContext _ctx;
        public TrainingUnitRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<TrainingUnit>> GetAllAsync()
            => await _ctx.TrainingUnits.ToListAsync();

        public async Task<TrainingUnit?> GetByIdAsync(long id)
            => await _ctx.TrainingUnits.FindAsync(id);

        public async Task<TrainingUnit> CreateAsync(TrainingUnit e)
        {
            await _ctx.TrainingUnits.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<TrainingUnit> UpdateAsync(TrainingUnit e)
        {
            _ctx.TrainingUnits.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

      

        public Task SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
