using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerrakiBanking.Business.Services.Interface;
using VerrakiBanking.Common.Enums;
using VerrakiBanking.Data.DbContext;
using VerrakiBanking.Data.Entity;

namespace VerrakiBanking.Business.Services.Implemetation
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get the balance of the account
        public async Task<decimal> GetBalanceAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new Exception("Account not found.");
            return account.Balance;
        }

        // Deposit into the account
        public async Task<decimal> DepositAsync(int accountId, decimal amount, string description)
        {
            if (amount <= 0)  // Check for negative or zero amount
                throw new Exception("Withdrawal amount must be greater than zero.");
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new Exception("Account not found.");

            // Update balance
            account.Balance += amount;

            // Record the transaction
            var transaction = new Transactions
            {
                AccountId = accountId,
                Amount = amount,
                Date = DateTime.Now,
                TransactionType = TransactionType.Deposit,
                Description = description
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return account.Balance;
        }

        // Withdraw from the account
        public async Task<decimal> WithdrawAsync(int accountId, decimal amount, string description)
        {
            if (amount <= 0)  
                throw new Exception("Withdrawal amount must be greater than zero.");
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
                throw new Exception("Account not found.");

            if (account.Balance < amount)
                throw new Exception("Insufficient funds.");

            // Update balance
            account.Balance -= amount;

            // Record the transaction
            var transaction = new Transactions
            {
                AccountId = accountId,
                Amount = amount,
                Date = DateTime.Now,
                TransactionType = TransactionType.Withdrawal,
                Description = description
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return account.Balance;
        }
    }
}
