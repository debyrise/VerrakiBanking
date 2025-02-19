using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerrakiBanking.Data.DTOs;
using static VerrakiBanking.Business.Services.Implemetation.AuthService;

namespace VerrakiBanking.Business.Services.Interface
{
    public interface IAuthservice
    {
        Task<LoginResult> LoginUserAsync(LoginModel model);
        Task<IdentityResult> RegisterUserAsync(RegisterModel model);
    }
}
