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
        Task<decimal> GetBalanceAsync(string accountNo);
        Task<decimal> DepositAsync(string accountNo, decimal amount, string description);
        Task<decimal> WithdrawAsync(string accountNo, decimal amount, string description);
    }

}
