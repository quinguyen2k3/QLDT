using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository
{
    public interface TrainingFormatRepo
    {
        Task<IEnumerable<TrainingFormat>> GetAllAsync();
    }
}
