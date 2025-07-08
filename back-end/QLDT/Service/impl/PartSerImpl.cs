using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Repository;
using QLDT.Models;

namespace QLDT.Service.impl
{
    public class PartSerImpl : PartSer
    {
        private readonly PartRepo _repo;
        private readonly IMapper _mapper;

        public PartSerImpl(PartRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PartRes>>(list);
        }

        public async Task<PartRes?> GetByIdAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<PartRes>(e);
        }

        public async Task<PartRes> CreateAsync(PartReq req)
        {
            var e = _mapper.Map<Part>(req);
            e = await _repo.CreateAsync(e);
            return _mapper.Map<PartRes>(e);
        }

        public async Task<bool> UpdateAsync(long id, PartReq req)
        {
            var e = await _repo.GetByIdAsync(id);
            if (e == null) return false;
            _mapper.Map(req, e);
            await _repo.UpdateAsync(e);
            return true;
        }

      
    }
}
