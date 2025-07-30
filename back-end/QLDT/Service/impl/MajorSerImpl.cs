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
    public class MajorSerImpl : MajorSer
    {
        private readonly MajorRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionManager _transactionManager;

        public MajorSerImpl(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            MajorRepo repository,
            TransactionManager transactionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<MajorRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<MajorRes>>(entities);
            return result;
        }

        public async Task<IEnumerable<MajorRes>> GetAllActiveAsync()
        {
            var entities = await _repository.GetAllIsActiveAsync();
            var result = _mapper.Map<IEnumerable<MajorRes>>(entities);
            return result;
        }
        public async Task<MajorRes> CreateAsync(MajorReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var entity = _mapper.Map<Major>(request);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = username;
                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdEntity = await _repository.CreateAsync(entity);
                await _transactionManager.CommitAsync();

                return _mapper.Map<MajorRes>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating Part: " + ex.Message);
            }
        }

        public async Task<MajorRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<MajorRes>(entity);
        }

        public async Task<MajorRes?> UpdateAsync(long id, MajorReq request)
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

                return _mapper.Map<MajorRes>(updatedEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
