using QLDT.Data;
using QLDT.Models;
using Microsoft.EntityFrameworkCore;

namespace QLDT.Repository.impl
{
    public class FileCertificateRepoImpl : FileCertificateRepo
    {
        private readonly ApplicationDbContext _context;
        public FileCertificateRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FileCertificate>> SaveAllAsync(IEnumerable<FileCertificate> fileCetificates)
        {
            _context.FileCetificates.AddRange(fileCetificates);
            await _context.SaveChangesAsync();
            return fileCetificates;
        }

        public async Task DeleteByIdsAsync(List<string> ids)
        {
            var files = await _context.FileCetificates.Where(f => ids.Contains(f.Id)).ToListAsync();
            if (!files.Any()) return;

            _context.FileCetificates.RemoveRange(files);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FileCertificate>> GetByCetificateIdAsync(long id)
        {
            return await _context.FileCetificates
                .Where(fc => fc.CertificateId == id)
                .ToListAsync();
        }
    }
}
