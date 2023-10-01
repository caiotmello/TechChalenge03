using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Dtos.Validations;
using Application.Services;
using Application.Services.Interface;
using AutoMapper;
using Infrastructure.Identity.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _serviceToken;
        private readonly JwtOptions _jwtOptions;
        private readonly IMapper _mapper;

        public IdentityService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions, IMapper mapper, ITokenService serviceToken)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
            _serviceToken = serviceToken;
        }

        public async Task<ResultService<UserLoginResponseDto>> LoginAsync(UserLoginRequestDto userLoginDto)
        {
            if (userLoginDto == null)
                return ResultService.Fail<UserLoginResponseDto>("The Object is null!");

            var validation = new UserLoginRequestDtoValidator().Validate(userLoginDto);
            if (!validation.IsValid)
                return ResultService.RequestError<UserLoginResponseDto>("Validation Problem!", validation);

            var result = await _signInManager.PasswordSignInAsync(userLoginDto.Email, userLoginDto.Password, false, true);
            if (!result.Succeeded)
                return ResultService.Fail<UserLoginResponseDto>("User not Authenticated - Incorrect User or Password!");

            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            var token = await _serviceToken.GenerateAsync(user);

            var responseDto = new UserLoginResponseDto
                (
                    token: token,
                    expiryDate: DateTime.Now.AddSeconds(_jwtOptions.Expiration)
                );

            return ResultService.Ok<UserLoginResponseDto>(responseDto);   
        }

        public async Task<ResultService> SignUpAsync(UserSignUpRequestDto userSignUpDto)
        {
            if (userSignUpDto == null)
                return ResultService.Fail("The Object is null!");

            var validation = new UserSignUpRequestDtoValidator().Validate(userSignUpDto);
            if (!validation.IsValid)
                return ResultService.RequestError<UserLoginResponseDto>("Validation Problem!", validation);

            var user = _mapper.Map<IdentityUser>(userSignUpDto);
            user.UserName = userSignUpDto.Email;

            var result = await _userManager.CreateAsync(user, userSignUpDto.Password);
            if (!result.Succeeded)
                return ResultService.RequestError("Sign Up Problem!", result);
                
            await _userManager.SetLockoutEnabledAsync(user, false);

            return ResultService.Ok("User signed up!");
        }
    }
}
