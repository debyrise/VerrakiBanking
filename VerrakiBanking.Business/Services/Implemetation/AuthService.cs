using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VerrakiBanking.Business.Services.Interface;
using VerrakiBanking.Data.DbContext;
using VerrakiBanking.Data.DTOs;
using VerrakiBanking.Data.Entity;

namespace VerrakiBanking.Business.Services.Implemetation
{
    public class AuthService : IAuthservice
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                TransactionPin = model.TransactionPin
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var accountNumber = GenerateAccountNumber();

                var account = new Accounts
                {
                    AccountNumber = accountNumber,
                    Balance = 0.00m,
                    AccountHolder = $"{user.FirstName} {user.LastName}",
                    CreatedAt = DateTime.Now,
                    AccountType = "Savings",
                    IsActive = true
                };

                _dbContext.Accounts.Add(account);
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }

        private string GenerateAccountNumber()
        {
            var random = new Random();
            var randomNumber = random.Next(10000000, 99999999);
            return $"00{randomNumber}";
        }

        public async Task<LoginResult> LoginUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new LoginResult { Succeeded = false, Message = "Invalid credentials." };

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
                return new LoginResult { Succeeded = false, Message = "Invalid credentials." };

            // Generate JWT Token if login is successful
            var token = GenerateJwtToken(user); // Calling the method to generate the token

            return new LoginResult { Succeeded = true, Message = "Login successful!", Token = token };
        }


        // Method to generate the JWT token
        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // User's name
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token
        new Claim(ClaimTypes.NameIdentifier, user.Id), // User's ID
        // Add any other claims like user roles or email as needed
         };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])); // Secret key from config
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // Issuer from config
                audience: _configuration["Jwt:Audience"], // Audience from config
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiration time
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token); // Returns the JWT token as a string
        }

        public class LoginResult
        {
            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public string Token { get; set; }
        }

    }
}
