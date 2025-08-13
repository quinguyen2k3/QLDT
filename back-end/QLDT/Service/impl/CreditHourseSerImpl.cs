using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class CreditHourseSerImpl : CreditHourseSer
    {
        private readonly CreditHourseRepo _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionManager _transactionManager;

        public CreditHourseSerImpl(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            CreditHourseRepo repository,
            TransactionManager transactionManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _repository = repository;
            _transactionManager = transactionManager;
        }

        public async Task<IEnumerable<CreditHourseRes>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CreditHourseRes>>(entities);
            return result;
        }

        public async Task<IEnumerable<CreditHourseRes>> GetAllActiveAsync()
        {
            var entities = await _repository.GetAllIsActiveAsync();
            var result = _mapper.Map<IEnumerable<CreditHourseRes>>(entities);
            return result;
        }

        public async Task<CreditHourseRes> CreateAsync(CreditHourseReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var entity = _mapper.Map<CreditHourse>(request);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = username;
                entity.ModifiedDate = entity.CreatedDate;
                entity.ModifiedBy = entity.CreatedBy;

                var createdEntity = await _repository.CreateAsync(entity);
                await _transactionManager.CommitAsync();

                return _mapper.Map<CreditHourseRes>(createdEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error creating TrainingUnit: " + ex.Message);
            }
        }

        public async Task<CreditHourseRes?> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            return _mapper.Map<CreditHourseRes>(entity);
        }

        public async Task<CreditHourseRes?> UpdateAsync(long id, CreditHourseReq request)
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

                return _mapper.Map<CreditHourseRes>(updatedEntity);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
