using BackendCodeForML.Models;
using BackendCodeForML.Services;
using BackendCodeForML.Web.Models;
using CommonServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendCodeForML.Web.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public ServiceResult<RegisterVM> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return new ServiceResult<RegisterVM>()
                {
                    Data = null,
                    Message = "Validation failed",
                    Status = ResultStatus.processError
                };
            }

            var registerModel = new RegisterModel
            {
                Username = vm.Username,
                Email = vm.Email,
                Password = vm.Password,
                ConfirmPassword = vm.ConfirmPassword
            };

            var result = _userService.CreateAccount(registerModel);

            if (result.Status == ResultStatus.Ok)
            {
                return new ServiceResult<RegisterVM>()
                {
                    Message = result.Message,
                    Data = new RegisterVM
                    {
                        UserId = result.Data.UserId,
                        Username = result.Data.Username,
                        Email = result.Data.Email
                    }
                };
            }
            else
            {
                return new ServiceResult<RegisterVM>()
                {
                    Message = result.Message,
                    Status = ResultStatus.processError
                };
            }
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login(LoginModel login)
        {
            var response = await _userService.Login(login);
            return Ok(response);
        }

        [HttpPost("logout")]
        public  IActionResult Logout()
        {
            return Ok(new { message = "Logout successful" });
        }
    }
}
