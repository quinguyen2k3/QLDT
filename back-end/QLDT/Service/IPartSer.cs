using System.Collections.Generic;
using System.Threading.Tasks;
using QLDT.Dtos.request;
using QLDT.Dtos.response;

using QLDT.Models;

namespace QLDT.Service
{
    public interface IPartSer
    {
        Task<IEnumerable<PartRes>> GetAllAsync();
        Task<PartRes?> GetByIdAsync(long id);
        Task<PartRes> CreateAsync(PartReq req);
        Task<bool> UpdateAsync(long id, PartReq req);
        Task<bool> DeleteAsync(long id);
    }
}
