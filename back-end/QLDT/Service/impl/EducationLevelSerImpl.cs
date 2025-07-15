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
    public class EducationLevelSerImpl : EducationLevelSer
    {
        private readonly EducationLevelRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionManager _transactionManager;

        public EducationLevelSerImpl(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            EducationLevelRepo repository,
            TransactionManager transactionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<EducationLevelRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<EducationLevelRes>>(entities);
            return result;
        }

        public async Task<EducationLevelRes> CreateAsync(EducationLevelReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var entity = _mapper.Map<EducationLevel>(request);

                entity.CreatedBy = username;
                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdEntity = await _repository.CreateAsync(entity);
                await _transactionManager.CommitAsync();

                return _mapper.Map<EducationLevelRes>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating EducationLevel: " + ex.Message);
            }
        }

        public async Task<EducationLevelRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<EducationLevelRes>(entity);
        }

        public async Task<EducationLevelRes?> UpdateAsync(long id, EducationLevelReq request)
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

                _mapper.Map(request, existing);

                existing.ModifiedDate = DateTime.Now;
                existing.ModifiedBy = username;

                var updatedEntity = await _repository.UpdateAsync(existing);
                await _transactionManager.CommitAsync();

                return _mapper.Map<EducationLevelRes>(updatedEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
