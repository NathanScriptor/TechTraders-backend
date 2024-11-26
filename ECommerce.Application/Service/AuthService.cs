using AutoMapper;
using ECommerce.Application.DTOs.Auth;
using ECommerce.Application.DTOs.User;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;
using ECommerce.Application.Others;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ECommerce.Application.Service
{
    public class AuthService(IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService) : IAuthService
    {
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<GetUserDto> GetInfo()
        {
            string userId = Authentication.GetUserIdFromHttpContextAccessor(_contextAccessor);
            return _mapper.Map<GetUserDto>(await _unitOfWork.GetRepository<User>().GetByIdAsync(userId));
        }

        public async Task<GetSignInDto> SignIn([FromBody] SignInDto request)
        {
            User? user = await _unitOfWork.GetRepository<User>().Entities
                .FirstOrDefaultAsync(p => p.Username == request.Username);

            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ErrorCode.UnAuthorized, "This username is not registered! Please sign up!");
            }

            if (user.Password != HashPassword.HashPasswordThrice(request.Password))
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ErrorCode.UnAuthorized, "Incorrect password!");
            }

            // Check if a token already exists for the user
            var existingUserToken = await _unitOfWork.GetRepository<UserToken>().Entities
                .FirstOrDefaultAsync(ut => ut.UserId == user.Id);

            GetTokenDto token;

            if (existingUserToken != null)
            {
                // If token exists, use the existing one
                token = new GetTokenDto
                {
                    AccessToken = existingUserToken.RefreshToken, // Assuming you want to return the existing refresh token
                    RefreshToken = existingUserToken.RefreshToken // Modify as per your logic if different tokens are used
                };
            }
            else
            {
                // If token does not exist, generate new tokens
                token = _tokenService.GenerateTokens(user);

                UserToken userToken = new()
                {
                    UserId = user.Id,
                    RefreshToken = token.RefreshToken,
                };

                await _unitOfWork.GetRepository<UserToken>().InsertAsync(userToken);
                await _unitOfWork.SaveAsync();
            }

            return new GetSignInDto()
            {
                Person = _mapper.Map<GetUserDto>(user),
                Token = token
            };
        }


        public async Task SignUp([FromBody] SignUpDto model)
        {
            User? user = await _unitOfWork.GetRepository<User>().Entities.FirstOrDefaultAsync(p => p.Username == model.Username);
            if (user != null)
            {
                throw new ErrorException(StatusCodes.Status409Conflict, ErrorCode.Conflicted, "This mail have been registered! Please sign in!");
            }
            if (!Regex.IsMatch(model.Username, @"^[a-zA-Z0-9._%+-]+@gmail\.com$", RegexOptions.IgnoreCase))
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorCode.InvalidInput, "This is not a gmail!");
            }
            if (model.Password != model.ConfirmedPassword)
            {
                throw new ErrorException(StatusCodes.Status400BadRequest, ErrorCode.InvalidInput, "Your password and confirmed password is not the same!");
            }

            User newUser = new()
            {
                Id = Guid.NewGuid().ToString("N"),
                Username = model.Username,
                PhoneNumber = model.Phonenumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = HashPassword.HashPasswordThrice(model.Password),
            };  
            
            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            await _unitOfWork.SaveAsync();
        }

        public async Task<GetTokenDto> RefreshToken(string refreshToken)
        {
            var userToken = await _unitOfWork.GetRepository<UserToken>()
                .Entities
                .FirstOrDefaultAsync(ut => ut.RefreshToken == refreshToken);

            if (userToken == null)
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ErrorCode.UnAuthorized, "Invalid refresh token.");
            }

            var user = await _unitOfWork.GetRepository<User>().GetByIdAsync(userToken.UserId);
            if (user == null)
            {
                throw new ErrorException(StatusCodes.Status401Unauthorized, ErrorCode.UnAuthorized, "User not found.");
            }

            GetTokenDto newTokens = _tokenService.GenerateTokens(user);

            userToken.RefreshToken = newTokens.RefreshToken;
            await _unitOfWork.GetRepository<UserToken>().UpdateAsync(userToken);
            await _unitOfWork.SaveAsync();

            return newTokens;
        }
    }
}
