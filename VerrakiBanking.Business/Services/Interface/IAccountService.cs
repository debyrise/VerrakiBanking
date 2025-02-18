using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerrakiBanking.Business.Services.Implemetation;

namespace VerrakiBanking.Business.Services.Interface
{

    public interface IAccountService
    {
        Task<decimal> GetBalanceAsync(int accountId);
        Task<decimal> DepositAsync(int accountId, decimal amount, string description);
        Task<decimal> WithdrawAsync(int accountId, decimal amount, string description);
    }

}
