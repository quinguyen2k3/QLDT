using QLDT.Data;
using QLDT.Models;
using Microsoft.EntityFrameworkCore;

namespace QLDT.Repository.impl
{
    public class RefreshTokenRepoImpl : RefreshTokenRepo
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepoImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> CreateAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken> UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<List<RefreshToken>> GetExpiredAsync(CancellationToken ct)
        {
            return await _context.RefreshTokens
                .Where(rt => rt.ExpiredAt < DateTime.Now)
                .ToListAsync(ct);
        }

        public async Task DeleteAsync(IEnumerable<RefreshToken> tokens, CancellationToken ct)
        {
            _context.RefreshTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync(ct);
        }
    }
}
