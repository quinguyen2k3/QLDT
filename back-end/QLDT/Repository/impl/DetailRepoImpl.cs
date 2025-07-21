using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class DetailRepoImpl : DetailRepo
    {
        private readonly ApplicationDbContext _context;

        public DetailRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAllAsync(IEnumerable<Detail> details)
        {
            await _context.Details.AddRangeAsync(details);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByClassIdAsync(long classId)
        {
            var existing = await _context.Details
                .Where(x => x.ClassId == classId)
                .ToListAsync();

            _context.Details.RemoveRange(existing);
        }

        public async Task<IEnumerable<Detail>> GetByClassIdAsync(long id)
        {
            return await _context.Details
                .Where(dt => dt.ClassId == id)
                .ToListAsync();
        }
    }
}
