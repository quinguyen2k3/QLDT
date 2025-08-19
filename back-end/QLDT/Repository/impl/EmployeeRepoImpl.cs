using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Runtime.InteropServices;

namespace QLDT.Repository.impl
{
    public class EmployeeRepoImpl : EmployeeRepo
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Level)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllByUsernameAsync(string username)
        {
            return await _context.Employees
                .Where(e => e.CreatedBy == username)
                .Include(e => e.Department)
                .Include(e => e.Level)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllByDepartmentIdAsync(long id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Level)
                .Where(e => e.DepId == id && e.IsActive == true)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(long id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Level)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee> CreateAsync(Employee e)
        {
            _context.Employees.Add(e);
            await _context.SaveChangesAsync();
            return e;
        }

        public async Task<Employee> UpdateAsync(Employee e)
        {
            _context.Employees.Update(e);
            await _context.SaveChangesAsync();
            return e;
        }
    }
}
