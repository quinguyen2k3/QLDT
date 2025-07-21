using AutoMapper;
using QLDT.Dtos.response;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class RoleSerImpl : RoleSer
    {
        private readonly RoleRepo _repository;
        private readonly IMapper _mapper;

        public RoleSerImpl( IMapper mapper, RoleRepo repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<RoleRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<RoleRes>>(entities);
            return result;
        }
    }
}
