using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices
{
    public record LoginResponse(bool Flag, string? AccessToken, string? Message);
}
