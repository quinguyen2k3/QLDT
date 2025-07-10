using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service.impl
{
    public class TrainingUnitSerImpl : TrainingUnitSer
    {
        private readonly TrainingUnitRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionManager _transactionManager;

        public TrainingUnitSerImpl(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            TrainingUnitRepo repository,
            TransactionManager transactionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<TrainingUnitRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<TrainingUnitRes>>(entities);
            return result;
        }

        public async Task<TrainingUnitRes> CreateAsync(TrainingUnitReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var entity = _mapper.Map<TrainingUnit>(request);
                entity.CreatedDate = request.CreatedDate ?? DateTime.Now;
                entity.CreatedBy = username;
                entity.CreatedDate = request.CreatedDate ?? DateTime.Now;
                entity.CreatedBy = username;
                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdEntity = await _repository.CreateAsync(entity);
                await _transactionManager.CommitAsync();

                return _mapper.Map<TrainingUnitRes>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating TrainingUnit: " + ex.Message);
            }
        }

        public async Task<TrainingUnitRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<TrainingUnitRes>(entity);
        }

        public async Task<TrainingUnitRes?> UpdateAsync(long id, TrainingUnitReq request)
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

                var updatedEntity = await _repository.UpdateAsync(existing);
                await _transactionManager.CommitAsync();

                return _mapper.Map<TrainingUnitRes>(updatedEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}