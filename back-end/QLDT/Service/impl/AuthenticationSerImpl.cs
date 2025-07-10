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

        private readonly PasswordHasher<User> _passwordHasher = new();
        public AuthenticationSerImpl(UserRepo userRepository, JwtManager jwtManager)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
        }

        public async Task<AuthenticationRes> AuthenticateAsync(AuthenticationReq request)
        {
            User user = await _userRepository.GetUserByUsernameAsync(request.username);

            if (user == null)
            {
                throw new Exception("Unauthenticated");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, request.password);
            bool authenticated = result == PasswordVerificationResult.Success ? true : false;

            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("Unauthenticated");
            }

            var token = await _jwtManager.GenerateAccessTokenAsync(user);

            return new AuthenticationRes
            {
                accessToken = token.accessToken,
                refreshToken = token.refreshToken,
                authenticated = authenticated
            };
        }

        public async Task LogoutAsync(TokenDto request)
        {
            if (request == null)
                throw new Exception("Request is invalid");
            await _jwtManager.RevokeTokenAsync(request);
        }

    }   
}
