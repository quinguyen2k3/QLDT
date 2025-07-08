using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class TrainingFormatRepoImpl : TrainingFormatRepo
    {
        private readonly ApplicationDbContext _ctx;
        public TrainingFormatRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<TrainingFormat>> GetAllAsync()
            => await _ctx.TrainingFormats.ToListAsync();

        public async Task<TrainingFormat?> GetByIdAsync(long id)
            => await _ctx.TrainingFormats.FindAsync(id);

        public async Task<TrainingFormat> CreateAsync(TrainingFormat e)
        {
            await _ctx.TrainingFormats.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<TrainingFormat> UpdateAsync(TrainingFormat e)
        {
            _ctx.TrainingFormats.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

       

        public Task SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}
