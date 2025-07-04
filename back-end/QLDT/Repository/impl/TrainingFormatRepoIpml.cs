using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Repository.impl
{
   
    public class TrainingFormatRepoIpml : TrainingFormatRepo
    {
        private readonly ApplicationDbContext _context;
        public TrainingFormatRepoIpml(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingFormat>> GetAllAsync()
        {
            return await _context.TrainingFormats.ToListAsync();
        }
    }
}
