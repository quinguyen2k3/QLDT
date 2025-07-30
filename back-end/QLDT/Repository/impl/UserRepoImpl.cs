using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository.impl
{
    public class UserRepoImpl : UserRepo
    {
        private readonly ApplicationDbContext _context;
        public UserRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByIdAsync(long Id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == Id);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user; 
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }


    }
}
