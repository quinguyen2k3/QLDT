using QLDT.Models;

namespace QLDT.Repository
{
    public interface InvalidTokenRepo
    {
        Task CreateAsync(InvalidToken token);
        Task<bool> ExistsAsync(string jti);
    }
}
