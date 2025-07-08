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
        private readonly IHttpContextAccessor _http;

        public DepartmentSerImpl(DepartmentRepo repo, IMapper mapper, IHttpContextAccessor http)
        {
            _repo = repo;
            _mapper = mapper;
            _http = http;
        }

        public async Task<IEnumerable<DepartmentRes>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmentRes>>(list);
        }

        public async Task<DepartmentRes?> GetByIdAsync(long id)
        {
            var ent = await _repo.GetByIdAsync(id);
            return ent == null ? null : _mapper.Map<DepartmentRes>(ent);
        }

        public async Task<DepartmentRes> CreateAsync(DepartmentReq req)
        {
            var ent = _mapper.Map<Department>(req);
            // gán audit
            var uid = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ent.CreatedById = int.TryParse(uid, out var x) ? x : 0;
            ent.CreatedDate = DateTime.UtcNow;
            ent = await _repo.CreateAsync(ent);
            return _mapper.Map<DepartmentRes>(ent);
        }

        public async Task<bool> UpdateAsync(long id, DepartmentReq req)
        {
            var ent = await _repo.GetByIdAsync(id);
            if (ent == null) return false;
            _mapper.Map(req, ent);
            await _repo.UpdateAsync(ent);
            return true;
        }
    }
}