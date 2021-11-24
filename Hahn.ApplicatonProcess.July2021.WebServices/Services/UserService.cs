using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Hahn.ApplicatonProcess.July2021.Domain.Common;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Dtos;
using AutoMapper;
using Hahn.ApplicatonProcess.July2021.Domain.Validators;

namespace Hahn.ApplicatonProcess.July2021.WebServices.Services
{
    public interface IUserService
    {
        Task<CreateUserResposeDto> Create(CreateUserDto dto);
        Task<CreateUserResposeDto> Update(int id, CreateUserDto dto);
        Task<DeleteResponseDto> Delete(int id);
        Task<TokenResponseDto> AuthenticateAsync(TokenRequestDto dto);
        Task<User> GetById(int userId, bool includeAssets = false);
    }
    public class UserService : BaseService, IUserService
    {
        readonly IMapper _mapper;

        public UserService(IApplicationUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<CreateUserResposeDto> Create(CreateUserDto dto)
        {
            try
            {
                var validator = new AddUpdateUserValidator();

                var validatorResult = await validator.ValidateAsync(dto);

                if (!validatorResult.IsValid)
                {
                    var errorResult = new CreateUserResposeDto();
                    validatorResult.Errors.ForEach(x =>
                    {
                        errorResult.Message += $"{x.ErrorMessage};";
                    });
                    return errorResult;
                }

                var userRepo = unitOfWork.GenericRepository<User>();

                var userExist = await userRepo.FirstByConditionAsync(x => x.Email.Equals(dto.Email));

                if (userExist is not null)
                    return new CreateUserResposeDto { Message = "User exist!" };

                var newUser = _mapper.Map<User>(dto);

                var passwordHasher = new PasswordHasher<User>();
                newUser.HashedPassword = passwordHasher.HashPassword(newUser, dto.Password);

                newUser = await userRepo.InsertAsync(newUser);
                var result = _mapper.Map<User, CreateUserResposeDto>(newUser);
                await unitOfWork.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<CreateUserResposeDto> Update(int id, CreateUserDto dto)
        {
            var validator = new AddUpdateUserValidator();

            var validatorResult = await validator.ValidateAsync(dto);

            if (!validatorResult.IsValid)
            {
                var errorResult = new CreateUserResposeDto();
                validatorResult.Errors.ForEach(x =>
                {
                    errorResult.Message += $"{x.ErrorMessage};";
                });
                return errorResult;
            }

            var userRepo = unitOfWork.GenericRepository<User>();

            var userExist = await userRepo.FirstByConditionAsync(x => x.Id != id && x.Email.Equals(dto.Email));

            if (userExist is not null)
                return new CreateUserResposeDto { Message = "Email already exist!" };

            var model = await userRepo.GetAsync(id, false);
            if (model is null)
                return new CreateUserResposeDto { Message = "User not exist!" };

            model = _mapper.Map<User>(dto);
            model.Id = id;

            var passwordHasher = new PasswordHasher<User>();
            model.HashedPassword = passwordHasher.HashPassword(model, dto.Password);

            var result = _mapper.Map<CreateUserResposeDto>(model);

            await unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<TokenResponseDto> AuthenticateAsync(TokenRequestDto dto)
        {
            var userRepo = unitOfWork.GenericRepository<User>();
            var user = await userRepo.FirstByConditionAsync(x => x.Email == dto.Email);

            // return message if user not found
            if (user == null)
                return new TokenResponseDto { Message = "User not found" };

            var passwordHasher = new PasswordHasher<User>();
            // return message if password not matched
            if (passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password) == PasswordVerificationResult.Failed)
                return new TokenResponseDto { Message = "Password is not corrent" };

            // authentication successful so generate jwt token
            return GenerateJwtToken(user);
        }

        public Task<User> GetById(int userId, bool includeAssets = false)
        {
            return unitOfWork.GenericRepository<User>().GetAsync(userId, includes: includeAssets ? "Assets" : null);
        }

        public async Task<DeleteResponseDto> Delete(int id)
        {
            var userRepo = unitOfWork.GenericRepository<User>();

            var model = await userRepo.GetAsync(id, false);

            if (model is null)
                return new DeleteResponseDto { Message = "User does not exist!" };

            await userRepo.SoftDeleteAsync(id);

            return new DeleteResponseDto { Deleted = true, Message = "Deleted successfully" };
        }

        private TokenResponseDto GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Consts.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(Consts.TokenExpireDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResponseDto
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                ExpireAt = tokenDescriptor.Expires.Value,
                Message = "success"
            };
        }


    }
}
