using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class DepartmentRepoImpl : DepartmentRepo
    {
        private readonly ApplicationDbContext _ctx;
        public DepartmentRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Department>> GetAllAsync()
            => await _ctx.Departments
                 .Include(d => d.Part)
                 .OrderByDescending(x => x.CreatedDate)
                 .ToListAsync();
        public async Task<IEnumerable<Department>> GetAllIsActiveAsync()
          => await _ctx.Departments
               .Where(d => d.IsActive == true)
               .Include(d => d.Part)
               .OrderBy(c => c.Name)
               .ToListAsync();

        public async Task<IEnumerable<Department>> GetAllByUserAsync(string username)
           => await _ctx.Departments
                .Where(d => d.CreatedBy == username)
                .Include(d => d.Part)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

        public async Task<Department> CreateAsync(Department e)
        {
            await _ctx.Departments.AddAsync(e);
            await _ctx.SaveChangesAsync();
            return e;
        }

        public async Task<Department?> GetByIdAsync(long id)
            => await _ctx.Departments
                    .Include(d => d.Part)
                    .FirstOrDefaultAsync(d => d.Id == id);


        public async Task<Department> UpdateAsync(Department e)
        {
            _ctx.Departments.Update(e);
            await _ctx.SaveChangesAsync();
            return e;
        }
    }
}
