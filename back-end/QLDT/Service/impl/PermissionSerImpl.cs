using AutoMapper;
using QLDT.Dtos.response;
using QLDT.Models;
using QLDT.Repository;
using System.Security.Claims;

namespace QLDT.Service.impl
{
    public class PermissionSerImpl : PermissionSer
    {
        private readonly PermissionRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionSerImpl(PermissionRepo repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<PermissionRes>> GetAllByUserAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var role = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(role))
            {
                throw new UnauthorizedAccessException("Invalid user info in token.");
            }
            var entities = await _repository.GetAllByRolenameAsync(role);
            return _mapper.Map<IEnumerable<PermissionRes>>(entities);
        }
    }
}
