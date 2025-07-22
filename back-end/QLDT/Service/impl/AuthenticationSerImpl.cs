using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QLDT.Dtos;
using QLDT.Dtos.request;
using QLDT.Dtos.response;
using QLDT.Manager;
using QLDT.Models;
using QLDT.Repository;
namespace QLDT.Service.impl
{
    public class AuthenticationSerImpl : AuthenticationSer
    {
        private readonly UserRepo _userRepository;
        private readonly JwtManager _jwtManager;
        private readonly TransactionManager _transactionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly PasswordHasher<User> _passwordHasher = new();
        public AuthenticationSerImpl(UserRepo userRepository, JwtManager jwtManager, TransactionManager transactionManager ,IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
            _transactionManager = transactionManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationRes> AuthenticateAsync(AuthenticationReq request)
        {
            User user = await _userRepository.GetByUsernameAsync(request.username);


            if (user == null)
            {
                return new AuthenticationRes
                {
                    authenticated = false
                };
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.password);
            bool authenticated = result == PasswordVerificationResult.Success;

            if (!authenticated)
            {
                return new AuthenticationRes
                {
                    authenticated = false
                };
            }

            var token = await _jwtManager.GenerateAccessTokenAsync(user);

            return new AuthenticationRes
            {
                accessToken = token.accessToken,
                refreshToken = token.refreshToken,
                authenticated = true
            };
        }

        public async Task LogoutAsync(TokenDto request)
        {
            if (request == null)
                throw new Exception("Request is invalid");
            await _jwtManager.RevokeTokenAsync(request);
        }

        public async Task ChangePasswordAsync(ChangePasswordReq request)
        {
            await _transactionManager.BeginTransactionAsync();
            try
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                var username = claimsPrincipal?.FindFirst("username")?.Value;
                if (string.IsNullOrEmpty(username))
                    throw new UnauthorizedAccessException("Invalid user info in token.");

                var user = await _userRepository.GetByUsernameAsync(username);
                if (user == null)
                    throw new Exception("User not exist");


                user.Password = _passwordHasher.HashPassword(user, request.Password);
                await _userRepository.UpdateAsync(user);

                await _transactionManager.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transactionManager.RollbackAsync();
                throw new Exception("Error updating class: " + ex.Message, ex);
            }

        }

    }
}