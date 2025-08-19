using QLDT.Models;

namespace QLDT.Repository
{
    public interface FileCertificateRepo
    {
        Task<IEnumerable<FileCertificate>> SaveAllAsync(IEnumerable<FileCertificate> fileCetificates);
        Task DeleteByIdsAsync(List<string> ids);
        Task<IEnumerable<FileCertificate>> GetByCetificateIdAsync(long id);
    }
}
