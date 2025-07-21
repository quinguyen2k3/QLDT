using QLDT.Data;
using QLDT.Models;
using Microsoft.EntityFrameworkCore;

namespace QLDT.Repository.impl
{
    public class FileClassesRepoImpl : FileClassesRepo
    {
        private readonly ApplicationDbContext _context;
        public FileClassesRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FileClass>> SaveAllAsync(IEnumerable<FileClass> fileClasses)
        {
            _context.FileClasses.AddRange(fileClasses);
            await _context.SaveChangesAsync();
            return fileClasses;
        }

        public async Task DeleteByIdsAsync(List<string> ids)
        {
            var files = await _context.FileClasses.Where(f => ids.Contains(f.Id)).ToListAsync();
            if (!files.Any()) return;

            _context.FileClasses.RemoveRange(files);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FileClass>> GetByClassIdAsync(long id)
        {
            return await _context.FileClasses
                .Where(fc => fc.ClassId == id)
                .ToListAsync();
        }
    }
}
