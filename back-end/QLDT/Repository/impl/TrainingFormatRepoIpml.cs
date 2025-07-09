using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository.impl
{

    public class TrainingFormatRepoIpml : TrainingFormatRepo
    {
        private readonly ApplicationDbContext _context;
        public TrainingFormatRepoIpml(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingFormat>> GetAllAsync()
        {
            return await _context.TrainingFormats.ToListAsync();
        }

        public async Task<TrainingFormat> CreateAsync(TrainingFormat entity)
        {
            await _context.TrainingFormats.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TrainingFormat?> GetByIdAsync(long id)
        {
            return await _context.TrainingFormats
                                 .FirstOrDefaultAsync(tf => tf.Id == id);
        }

        public async Task<TrainingFormat> UpdateAsync(TrainingFormat entity)
        {
            _context.TrainingFormats.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}