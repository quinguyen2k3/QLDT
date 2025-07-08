using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Repository;
using QLDT.Models;
namespace QLDT.Service.impl
{
    public class EducationLevelSerImpl : EducationLevelSer
    {
        private readonly EducationLevelRepo _repo;
        private readonly IMapper _mapper;

        public EducationLevelSerImpl(EducationLevelRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EducationLevelRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<EducationLevelRes>>(list);
        }

        public async Task<EducationLevelRes?> GetByIdAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<EducationLevelRes>(e);
        }

        public async Task<EducationLevelRes> CreateAsync(EducationLevelReq req)
        {
            var e = _mapper.Map<EducationLevel>(req);
            e = await _repo.CreateAsync(e);
            return _mapper.Map<EducationLevelRes>(e);
        }

        public async Task<bool> UpdateAsync(long id, EducationLevelReq req)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            _mapper.Map(req, e);
            await _repo.UpdateAsync(e);
            return true;
        }

    }
}
