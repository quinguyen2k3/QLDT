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

        public async Task<IEnumerable<Department>> GetAllAsync() =>
            await _ctx.Departments.ToListAsync();

        public async Task<Department?> GetByIdAsync(long id) =>
            await _ctx.Departments.FindAsync(id);

        public async Task<Department> CreateAsync(Department entity)
        {
            await _ctx.Departments.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task<Department> UpdateAsync(Department entity)
        {
            _ctx.Departments.Update(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }
    }
}
