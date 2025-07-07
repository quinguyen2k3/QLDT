using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class TrainingUnitRepoImpl : ITrainingUnitRepo
    {
        private readonly ApplicationDbContext _ctx;
        public TrainingUnitRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<TrainingUnit>> GetAllAsync()
            => await _ctx.TrainingUnits.ToListAsync();

        public async Task<TrainingUnit?> GetByIdAsync(long id)
            => await _ctx.TrainingUnits.FindAsync(id);

        public async Task<TrainingUnit> CreateAsync(TrainingUnit entity)
        {
            await _ctx.TrainingUnits.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<TrainingUnit> UpdateAsync(TrainingUnit entity)
        {
            _ctx.TrainingUnits.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TrainingUnit entity)
        {
            _ctx.TrainingUnits.Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
            => _ctx.SaveChangesAsync();
    }
}
