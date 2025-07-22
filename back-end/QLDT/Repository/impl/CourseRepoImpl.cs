using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class CourseRepoImpl : CourseRepo
    {
        private readonly ApplicationDbContext _context;
        public CourseRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllByUsernameAsync(string username)
        {
            return await _context.Courses
                .Where(x => x.CreatedBy == username)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllIsActiveAsync()
        {
            return await _context.Courses
                .Where(x => x.IsActive == true)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(long id)
        {
            return await _context.Courses
                .Include(c => c.FileCourses)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> SaveAsync(Course e)
        {
            _context.Courses.Add(e);
            await _context.SaveChangesAsync();
            return e;
        }

        public async Task<Course> UpdateAsync(Course e)
        {
            _context.Courses.Update(e);
            await _context.SaveChangesAsync();
            return e;
        }
    }
}
