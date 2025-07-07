using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Repository;
using QLDT.Models;

namespace QLDT.Service.impl
{
    public class TrainingUnitSerImpl : TrainingUnitSer
    {
        private readonly TrainingUnitRepo _repo;
        private readonly IMapper _mapper;

        public TrainingUnitSerImpl(TrainingUnitRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingUnitRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainingUnitRes>>(list);
        }

        public async Task<TrainingUnitRes?> GetByIdAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<TrainingUnitRes>(e);
        }

        public async Task<TrainingUnitRes> CreateAsync(TrainingUnitReq req)
        {
            var e = _mapper.Map<TrainingUnit>(req);
            e = await _repo.CreateAsync(e);
            return _mapper.Map<TrainingUnitRes>(e);
        }

        public async Task<bool> UpdateAsync(long id, TrainingUnitReq req)
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
