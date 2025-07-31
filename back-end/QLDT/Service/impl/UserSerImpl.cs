using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;
using QLDT.Exceptions;

namespace QLDT.Service.impl
{
    public class UserSerImpl : UserSer
    {
        private readonly UserRepo _userRepository;
        private readonly IMapper _mapper;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserSerImpl(UserRepo userRepository, IMapper mapper, TransactionManager transactionManager, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _transactionManager = transactionManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<UserRes>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserRes>>(users);
        }

        public async Task<UserRes?> GetByIdAsync(long id)
        {
            var users = await _userRepository.GetByIdAsync(id);
            if (users == null) return null;
            return _mapper.Map<UserRes>(users);
        }

        public async Task<UserRes> CreateAsync(UserReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var currentUser = _httpContextAccessor.HttpContext?.User;
                var username = currentUser?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new Exception("Password is empty");

                var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
                if (existingUser != null)
                    throw new ConflictException("Username already exists.");

                var user = _mapper.Map<User>(request);

                user.Password = _passwordHasher.HashPassword(user, request.Password);

                user.CreatedBy = username;
                user.CreatedDate = DateTime.Now;

                user.ModifiedBy = username;
                user.ModifiedDate = DateTime.Now;

                user = await _userRepository.CreateAsync(user);
                await _transactionManager.CommitAsync();

                return _mapper.Map<UserRes>(user);
            }
            catch (Exception)
            {
                await _transactionManager.RollbackAsync();
                throw;
            }
        }

        public async Task<UserRes> UpdateAsync(long id, UserReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    throw new NotFoundException("User not found.");

                var currentUser = _httpContextAccessor.HttpContext?.User;
                var username = currentUser?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");


                var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
                if (existingUser != null && existingUser.Id != user.Id)
                {
                    throw new ConflictException("Username already exists.");
                }

                _mapper.Map(request, user); // cập nhật dữ liệu vào entity

                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    user.Password = _passwordHasher.HashPassword(user, request.Password);
                }

                user.ModifiedBy = username;
                user.ModifiedDate = DateTime.Now;

                await _userRepository.UpdateAsync(user);
                await _transactionManager.CommitAsync();

                return _mapper.Map<UserRes>(user);
            }
            catch (Exception)
            {
                await _transactionManager.RollbackAsync();
                throw; 
            }
        }
    }
}