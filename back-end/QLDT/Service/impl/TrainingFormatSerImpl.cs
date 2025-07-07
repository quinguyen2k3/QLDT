using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Repository;
using QLDT.Models;
namespace QLDT.Service.impl
{
    public class TrainingFormatSerImpl : ITrainingFormatSer
    {
        private readonly ITrainingFormatRepo _repo;
        private readonly IMapper _mapper;

        public TrainingFormatSerImpl(ITrainingFormatRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingFormatRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainingFormatRes>>(list);
        }

        public async Task<TrainingFormatRes?> GetByIdAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<TrainingFormatRes>(e);
        }

        public async Task<TrainingFormatRes> CreateAsync(TrainingFormatReq req)
        {
            var e = _mapper.Map<TrainingFormat>(req);
            e = await _repo.CreateAsync(e);
            return _mapper.Map<TrainingFormatRes>(e);
        }

        public async Task<bool> UpdateAsync(long id, TrainingFormatReq req)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            _mapper.Map(req, e);
            await _repo.UpdateAsync(e);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            await _repo.DeleteAsync(e);
            return true;
        }
    }
}
