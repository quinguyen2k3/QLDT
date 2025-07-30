using QLDT.Models;
using System.Threading.Tasks;

namespace QLDT.Repository
{
    public interface RefreshTokenRepo
    {
        Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken> UpdateAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<List<RefreshToken>> GetExpiredAsync(CancellationToken ct);
        Task DeleteAsync(IEnumerable<RefreshToken> tokens, CancellationToken ct);
    }
}
