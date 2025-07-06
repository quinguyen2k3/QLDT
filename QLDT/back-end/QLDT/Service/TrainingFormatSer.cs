using QLDT.Dtos.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service
{
    public interface TrainingFormatSer
    {
        Task<IEnumerable<TrainingFormatRes>> GetAllAsync();
    }
}
