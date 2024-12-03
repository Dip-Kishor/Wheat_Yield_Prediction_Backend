using BackendCodeForML.Models;
using CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCodeForML.Services
{
    public interface IUserService
    {
        ServiceResult<RegisterModel> CreateAccount(RegisterModel register);
        Task<LoginResponse> Login(LoginModel login);
        public void Logout();
    }
}
