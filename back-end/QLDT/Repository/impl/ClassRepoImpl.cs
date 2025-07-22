using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Migrations;
using QLDT.Models;
using System.Security.Cryptography.Xml;

namespace QLDT.Repository.impl
{
    public class ClassRepoImpl : ClassRepo
    {
        private readonly ApplicationDbContext _context;
        public ClassRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllByTrainingFormatIdAsync(long id)
        {
            return await _context.Classes
                .Where(x => x.FormatId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetAllByTrainingFormatIdAndUsernameAsync(long id, string username)
        {
            return await _context.Classes
                .Where(x => x.FormatId == id && x.CreatedBy == username)
            .ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(long id)
        {
            return await _context.Classes
                .Include(x => x.Unit)
                .Include(x => x.Level)
                .Include(x => x.Format)
                .Include(x => x.Course)
                .Include(x => x.Details)
                .Include(x => x.FileClasses)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Class> SaveAsync(Class e)
        {
            _context.Classes.Add(e);
            await _context.SaveChangesAsync();
            return e;
        }
        public async Task<Class> UpdateAsync(Class e)
        {
            _context.Classes.Update(e);
            await _context.SaveChangesAsync();
            return e;
        }
    }
}
