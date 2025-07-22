using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLDT.Service.impl
{
    public class TrainingFormatSerImpl : TrainingFormatSer
    {
        private readonly TrainingFormatRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionManager _transactionManager;

        public TrainingFormatSerImpl(IHttpContextAccessor httpContextAccessor, IMapper mapper, TrainingFormatRepo repository, TransactionManager transactionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
            _transactionManager = transactionManager;
        }


        public async Task<IEnumerable<TrainingFormatRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<TrainingFormatRes>>(entities);

            return result;
        }

        public async Task<IEnumerable<TrainingFormatRes>> GetAllActiveAsync()
        {
            var entities = await _repository.GetAllIsActiveAsync();

            var result = _mapper.Map<IEnumerable<TrainingFormatRes>>(entities);

            return result;
        }

        public async Task<TrainingFormatRes> CreateAsync(TrainingFormatReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;

                var username = user?.FindFirst("username")?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    throw new UnauthorizedAccessException("Invalid user info in token.");
                }

                var entity = _mapper.Map<TrainingFormat>(request);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = username;

                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdEntity = await _repository.CreateAsync(entity);

                await _transactionManager.CommitAsync();

                var result = _mapper.Map<TrainingFormatRes>(createdEntity);

                return result;
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating TrainingFormat: " + ex.Message);
            }
        }

        public async Task<TrainingFormatRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            return _mapper.Map<TrainingFormatRes>(entity);
        }

        public async Task<TrainingFormatRes?> UpdateAsync(long id, TrainingFormatReq request)
        {
            await _transactionManager.BeginTransactionAsync();

            try
            {
                var user = _httpContextAccessor.HttpContext?.User;

                var username = user?.FindFirst("username")?.Value;

                if (string.IsNullOrEmpty(username))
                {
                    throw new UnauthorizedAccessException("Invalid user info in token.");
                }
                var existing = await _repository.GetByIdAsync(id);
                if (existing == null)
                {
                    return null;
                }

                _mapper.Map(request, existing);

                existing.ModifiedDate = DateTime.Now;
                existing.ModifiedBy = username;

                var updatedEntity = await _repository.UpdateAsync(existing);

                await _transactionManager.CommitAsync();
                // Trả về DTO
                return _mapper.Map<TrainingFormatRes>(updatedEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<IEnumerable<TrainingFormatRes>> GetFormat1And2Async()
        {
            var entities = await _repository.GetById1And2Async();

            var result = _mapper.Map<IEnumerable<TrainingFormatRes>>(entities);

            return result;
        }

    }


}