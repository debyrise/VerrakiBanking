using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        // Register a new user
        public async Task<IdentityResult> RegisterUserAsync(RegisterModel model)
        {
            // Create the user
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                TransactionPin = model.TransactionPin
            };

            // Create the user in the Identity system
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Generate a 10-digit account number starting with "00"
                var accountNumber = GenerateAccountNumber();

                // Create a new account for the user
                var account = new Accounts
                {
                    AccountNumber = accountNumber,
                    Balance = 0.00m, // Initialize balance to 0
                    AccountHolder = $"{user.FirstName} {user.LastName}", // Combine first and last name
                    CreatedAt = DateTime.Now, // Set the account creation date
                    AccountType = "Savings", // Default account type (can be customized)
                    IsActive = true // Account is active by default
                };

                // Add the account to the database
                _dbContext.Accounts.Add(account);
                await _dbContext.SaveChangesAsync(); // Save changes to the database
            }

            return result;
        }

        private string GenerateAccountNumber()
        {
            // Generate a 10-digit account number starting with "00"
            var random = new Random();
            var randomNumber = random.Next(10000000, 99999999); // Generate a random 8-digit number
            return $"00{randomNumber}"; // Prepend "00" to make it 10 digits
        }



        // Login a user
        public async Task<SignInResult> LoginUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        }
    }
}
