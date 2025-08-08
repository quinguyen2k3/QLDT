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
        private readonly DetailRepo _detailRepository;
        private readonly IMapper _mapper;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public EmployeeSerImpl(EmployeeRepo employeeRepository,
                     DetailRepo detailRepository,
                     IMapper mapper,
                     TransactionManager transactionManager,
                     IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            _detailRepository = detailRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<EmployeeRes>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeRes>>(employees);
        }

        public async Task<IEnumerable<EmployeeRes>> GetAllByUserAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var username = user?.FindFirst("username")?.Value;
            if (string.IsNullOrEmpty(username))
                throw new UnauthorizedAccessException("Invalid user info in token.");

            var employees = await _employeeRepository.GetAllByUsernameAsync(username);
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

        public async Task<IEnumerable<EmployeeRes>> GetAllByCurrentUserDepartmentAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var id = user?.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(id))
                throw new UnauthorizedAccessException("Invalid user info in token.");

            var employee = await _employeeRepository.GetByIdAsync(long.Parse(id));
            if (employee == null)
                return null;

            if (!employee.DepId.HasValue)
                throw new Exception("This employee has not been assigned to a department.");

            var employees = await _employeeRepository.GetAllByDepartmentIdAsync(employee.DepId.Value);
            return _mapper.Map<IEnumerable<EmployeeRes>>(employees);
        }

        public async Task<EmployeeDetailRes> GetEmployeeDetailAsync(long id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;

            var details = await _detailRepository.GetByEmployeeIdAsync(id);

            var result = new EmployeeDetailRes
            {
                EmployeeName = employee.Name,
                EmployeeMaCBVC = employee.EmMaCBVC,
                EmployeeChucVu = employee.EmChucVu,
                EmployeeChucDanh = employee.EmChucDanh,
                EmployeeNgaySinh = employee.EmNgaySinh,
                Classes = details.Select(d => new ClassDetailRes
                {
                    ClassName = d.Class.Name,
                    ClassContent = d.Class.Content,
                    ClassSoTiet = d.Class.ClassSoTiet,
                    ClassSoTinhChi = d.SoTinhChi
                }).ToList()
            };

            return result;
        }
    }
}
