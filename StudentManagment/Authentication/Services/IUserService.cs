using Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface IUserService
    {
        Task<LoginResponseDto> Login(LoginModelDto model);
    }
}
