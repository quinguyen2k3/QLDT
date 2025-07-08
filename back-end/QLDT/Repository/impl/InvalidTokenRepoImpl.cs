using QLDT.Data;
using QLDT.Models;
using Microsoft.EntityFrameworkCore;

namespace QLDT.Repository.impl
{
    public class InvalidTokenRepoImpl : InvalidTokenRepo
    {
        private readonly ApplicationDbContext _context;

        public InvalidTokenRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(InvalidToken token)
        {
            await _context.InvalidTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string jti)
        {
            return await _context.InvalidTokens.AnyAsync(t => t.Jti == jti);
        }
    }
}
