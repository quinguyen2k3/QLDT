using AutoMapper;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;

namespace QLDT.Service.impl
{
    public class EmployeeSerImpl : EmployeeSer
    {
        private readonly EmployeeRepo _employeeRepository;
        private readonly IMapper _mapper;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public EmployeeSerImpl(EmployeeRepo employeeRepository,
                     IMapper mapper,
                     TransactionManager transactionManager,
                     IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EmployeeRes>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeRes>>(employees);
        }

        public async Task<EmployeeRes?> GetByIdAsync(long id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;
            return _mapper.Map<EmployeeRes>(employee);
        }

        public async Task<EmployeeRes> CreateAsync(EmployeeReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var employee = _mapper.Map<Employee>(request);
                employee.CreatedBy = username;
                employee.CreatedDate = DateTime.UtcNow;

                employee.ModifiedBy = username;
                employee.ModifiedDate = DateTime.UtcNow;

                employee = await _employeeRepository.CreateAsync(employee);
                await _transactionManager.CommitAsync();

                return _mapper.Map<EmployeeRes>(employee);
            }
            catch(Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }

        public async Task<EmployeeRes?> UpdateAsync(long id, EmployeeReq request)
        {
            await _transactionManager.BeginTransactionAsync();

            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var username = user?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null) return null;

                var existing = _mapper.Map(request, employee);

                existing.ModifiedBy = username;
                existing.ModifiedDate = DateTime.UtcNow;

                existing = await _employeeRepository.UpdateAsync(existing);

                await _transactionManager.CommitAsync();

                return _mapper.Map<EmployeeRes>(existing);
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error: " + ex.Message);
            }
        }

    }
}
