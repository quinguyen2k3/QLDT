using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class CertificateRepoImpl : CertificateRepo
    {
        private readonly ApplicationDbContext _ctx;
        public CertificateRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Certificate>> GetAllByEmployeeIdAsync(long id)
        {
            return await _ctx.Cetificates
                .Where(p => p.EmpId == id)
                .Include(c => c.Unit)
                .Include(c => c.Class)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<Certificate> CreateAsync(Certificate entity)
        {
            _ctx.Cetificates.Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<Certificate?> GetByIdAsync(long id)
        {
            return await _ctx.Cetificates
                .Include(c => c.Unit)
                .Include(c => c.Class)
                .Include(c => c.FileCertificates)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Certificate> UpdateAsync(Certificate entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
