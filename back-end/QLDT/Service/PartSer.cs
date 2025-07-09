using QLDT.Dtos.request;
using QLDT.Dtos.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface PartSer
    {
        Task<IEnumerable<PartRes>> GetAllAsync();
        Task<PartRes> CreateAsync(PartReq req);
        Task<PartRes?> GetByIdAsync(long id);
        Task<PartRes?> UpdateAsync(long id, PartReq req);
    }
}