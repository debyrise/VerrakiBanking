using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerrakiBanking.Data.DTOs;

namespace VerrakiBanking.Business.Services.Interface
{
    public interface IAuthservice
    {
        Task<SignInResult> LoginUserAsync(LoginModel model);
        Task<IdentityResult> RegisterUserAsync(RegisterModel model);
    }
}
