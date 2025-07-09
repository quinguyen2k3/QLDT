using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class DepartmentSerImpl : DepartmentSer
    {
        private readonly DepartmentRepo _repo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentSerImpl(
            DepartmentRepo repo,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<DepartmentRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmentRes>>(list);
        }

        public async Task<DepartmentRes?> GetByIdAsync(long id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : _mapper.Map<DepartmentRes>(e);
        }

        public async Task<DepartmentRes> CreateAsync(DepartmentReq req)
        {
            var username = _httpContextAccessor.HttpContext?.User
                               .FindFirstValue(ClaimTypes.Name) ?? throw new UnauthorizedAccessException();

            var entity = _mapper.Map<Department>(req);
            entity.CreatedDate = req.CreatedDate ?? DateTime.UtcNow;
            entity.CreatedBy = username;
            entity.ModifiedDate = entity.CreatedDate;
            entity.ModifiedBy = username;

            var created = await _repo.CreateAsync(entity);
            return _mapper.Map<DepartmentRes>(created);
        }

        public async Task<DepartmentRes?> UpdateAsync(long id, DepartmentReq req)
        {
            var username = _httpContextAccessor.HttpContext?.User
                               .FindFirstValue(ClaimTypes.Name) ?? throw new UnauthorizedAccessException();

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(req, existing);
            existing.ModifiedDate = DateTime.UtcNow;
            existing.ModifiedBy = username;

            var updated = await _repo.UpdateAsync(existing);
            return _mapper.Map<DepartmentRes>(updated);
        }
    }
}
