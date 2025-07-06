using QLDT.Models;

namespace QLDT.Repository
{
    public interface IUserRepo
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
