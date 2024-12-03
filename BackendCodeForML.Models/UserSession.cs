using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCodeForML.Models
{
    public record UserSession(int UserId,string Username,string Email,string Role);
   
}
