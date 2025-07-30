using QLDT.Models;

namespace QLDT.Repository
{
    public interface InvalidTokenRepo
    {
        Task CreateAsync(InvalidToken token);
        Task<bool> ExistsAsync(string jti);
        Task<List<InvalidToken>> GetExpiredAsync(CancellationToken ct);
        Task DeleteAsync(IEnumerable<InvalidToken> tokens, CancellationToken ct);
    }
}
