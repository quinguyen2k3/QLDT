using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;

namespace QLDT.Repository.impl
{
    public class UserRepoImpl : IUserRepo
    {
        private readonly ApplicationDbContext _ctx;
        public UserRepoImpl(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<User> FindByUsernameAsync(string username)
            => await _ctx.Users.SingleOrDefaultAsync(u => u.Username == username);

        public async Task<User> GetByIdAsync(int id)
            => await _ctx.Users.FindAsync(id);

        public Task SaveChangesAsync()
            => _ctx.SaveChangesAsync();
    }
}
