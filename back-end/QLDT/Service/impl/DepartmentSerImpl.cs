using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class DepartmentSerImpl : DepartmentSer
    {
        private readonly DepartmentRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionManager _transactionManager;

        public DepartmentSerImpl(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            DepartmentRepo repository,
            TransactionManager transactionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<DepartmentRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<DepartmentRes>>(entities);
            return result;
        }

        public async Task<DepartmentRes> CreateAsync(DepartmentReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var entity = _mapper.Map<Department>(request);
                entity.CreatedDate = request.CreatedDate ?? DateTime.Now;
                entity.CreatedBy = username;
                entity.CreatedDate = request.CreatedDate ?? DateTime.Now;
                entity.CreatedBy = username;
                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdEntity = await _repository.CreateAsync(entity);
                await _transactionManager.CommitAsync();

                return _mapper.Map<DepartmentRes>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating Department: " + ex.Message);
            }
        }

        public async Task<DepartmentRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<DepartmentRes>(entity);
        }

        public async Task<DepartmentRes?> UpdateAsync(long id, DepartmentReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var existing = await _repository.GetByIdAsync(id);
                if (existing == null) return null;

                existing.Name = request.Name;
                existing.Note = request.Note;
                existing.CreatedDate = request.CreatedDate;
                existing.ModifiedDate = DateTime.Now;
                existing.ModifiedBy = username;
                existing.PartId = request.PartId;

                var updatedEntity = await _repository.UpdateAsync(existing);
                await _transactionManager.CommitAsync();

                return _mapper.Map<DepartmentRes>(updatedEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}