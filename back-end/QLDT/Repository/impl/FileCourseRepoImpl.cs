using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class FileCourseRepoImpl : FileCourseRepo
    {
        private readonly ApplicationDbContext _context;
        public FileCourseRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FileCourse>> SaveAllAsync(IEnumerable<FileCourse> fileCourses)
        {
            _context.FileCourses.AddRange(fileCourses);
            await _context.SaveChangesAsync();
            return fileCourses;
        }

        public async Task DeleteByIdsAsync(List<string> ids)
        {
            var files = await _context.FileCourses.Where(f => ids.Contains(f.Id)).ToListAsync();
            if (!files.Any()) return;

            _context.FileCourses.RemoveRange(files);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FileCourse>> GetByCourseIdAsync(long id)
        {
            return await _context.FileCourses
                .Where(fc => fc.CourseId == id)
                .ToListAsync();
        }
    }
}
