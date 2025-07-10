// QLDT/Repository/impl/TrainingUnitRepoImpl.cs
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository.impl
{
    public class TrainingUnitRepoImpl : TrainingUnitRepo
    {
        private readonly ApplicationDbContext _ctx;

        public TrainingUnitRepoImpl(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<TrainingUnit>> GetAllAsync()
        {
            return await _ctx.TrainingUnits
                             .AsNoTracking()
                             .ToListAsync();
        }

        public async Task<TrainingUnit?> GetByIdAsync(long id)
        {
            return await _ctx.TrainingUnits
                             .AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Id == id);
        }

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
    }
}