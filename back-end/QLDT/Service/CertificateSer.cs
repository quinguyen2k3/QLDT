using QLDT.Dtos.request;
using QLDT.Dtos.response;

namespace QLDT.Service
{
    public interface CertificateSer
    {
        Task<IEnumerable<CertificateRes>> GetAllByUserAsync(long? id = null);
        Task<CertificateRes> CreateAsync(CertificateReq request);
        Task<CertificateRes?> GetByIdAsync(long id);
        Task<CertificateRes> UpdateAsync(long id, CertificateReq request);
    }
}
