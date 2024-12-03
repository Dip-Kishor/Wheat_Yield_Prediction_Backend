using BackendCodeForML.Models;
using CommonServices;
using System;
using BackendCodeForML.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace BackendCodeForML.Services
{
    public class UserService :IUserService
    {
        private readonly WYPredictionContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(WYPredictionContext context, IConfiguration configuration,
            ILogger<UserService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public ServiceResult<RegisterModel> CreateAccount(RegisterModel model)
        {
            if (model == null)
            {
                return new ServiceResult<RegisterModel>
                {
                    Data = null,
                    Message = "Model is null",
                    Status = ResultStatus.processError
                };
            }

            var existingUser = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (existingUser != null)
            {
                return new ServiceResult<RegisterModel>
                {
                    Data = null,
                    Message = "User already exists",
                    Status = ResultStatus.processError
                };
            }

            if (!_context.Users.Any(x => x.RoleId == 1)) // No SuperAdmin exists
            {
                model.RoleId = 1; 
            }
            else if (!_context.Users.Any(x => x.RoleId == 2)) // No Admin exists
            {
                model.RoleId = 2; 
            }
            else
            {
                model.RoleId = 3; // Assign User role
            }

            var newUser = new RegisterModel
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
                RoleId = model.RoleId
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new ServiceResult<RegisterModel>
            {
                Data = newUser,
                Message = "User registered successfully",
                Status = ResultStatus.Ok
            };
        }

        
        public async Task<LoginResponse> Login(LoginModel model)
        {

            if(model == null)
            {
                return new LoginResponse(false, null!, "Invalid input");
                
            }

            var getUser = _context.Users.FirstOrDefault(x=>x.Email == model.Email);
            if(getUser == null)
            {
                return new LoginResponse(false, null!, "User not found");

            }

            if (getUser.Password != model.Password)
            {
                return new LoginResponse(false, null!, "Invalid Username/Password");
            }
            var userRole = _context.Users
            .Include(u => u.Role) 
            .FirstOrDefault(u => u.Email == model.Email);

            var roleName = userRole.Role.RoleName;

            var userSession = new UserSession(getUser.UserId, getUser.Username, getUser.Email, roleName);
            string accessToken = GenerateAccessToken(userSession);

            return new LoginResponse(true, accessToken, "User Logged in successfully");

        }
        public void Logout()
        {
            //var cookies = _httpContextAccessor.HttpContext.Request.Cookies.Keys;
            //foreach (var cookie in cookies)
            //{
            //    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
            //}
        }
        private string GenerateAccessToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AccessToken:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var accessToken = new JwtSecurityToken
            (
                issuer: _configuration["AccessToken:Issuer"],
                audience: _configuration["AccessToken:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddDays(1.5),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

    }
}
