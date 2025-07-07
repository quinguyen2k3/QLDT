using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Repository;
using QLDT.Models;
namespace QLDT.Service.impl
{
    public class RoleSerImpl : IRoleSer
    {
        private readonly IRoleRepo _repo;
        private readonly IMapper _mapper;

        public RoleSerImpl(IRoleRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleRes>>(list);
        }

        public async Task<RoleRes?> GetByIdAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<RoleRes>(e);
        }

        public async Task<RoleRes> CreateAsync(RoleReq req)
        {
            var e = _mapper.Map<Role>(req);
            e = await _repo.CreateAsync(e);
            return _mapper.Map<RoleRes>(e);
        }

        public async Task<bool> UpdateAsync(long id, RoleReq req)
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
