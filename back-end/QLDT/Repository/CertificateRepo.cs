using QLDT.Models;

namespace QLDT.Repository
{
    public interface CertificateRepo
    {
        Task<IEnumerable<Certificate>> GetAllByEmployeeIdAsync(long id);
        Task<Certificate> CreateAsync(Certificate entity);
        Task<Certificate?> GetByIdAsync(long id);
        Task<Certificate> UpdateAsync(Certificate entity);
    }
}
