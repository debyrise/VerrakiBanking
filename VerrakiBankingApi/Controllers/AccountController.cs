using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerrakiBanking.Business.Services.Interface;
using VerrakiBanking.Data.DTOs;

namespace VerrakiBankingApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpGet("balance/{accountNo}")]
        public async Task<IActionResult> GetBalance(string accountNo)
        {
            try
            {
                var balance = await _accountService.GetBalanceAsync(accountNo);
                return Ok(new { AccountId = accountNo, Balance = balance });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("deposit/{accountNo}")]
        public async Task<IActionResult> Deposit(string accountNo, [FromBody] DepositRequest request)
        {
            try
            {
                var newBalance = await _accountService.DepositAsync(accountNo, request.Amount, request.Description);

                if (newBalance >= 0)
                {
                    return Ok(new { Message = "Deposit successful", NewBalance = newBalance });
                }
                else
                {
                    return BadRequest(new { Message = "Deposit failed" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("withdraw/{accountNo}")]
        public async Task<IActionResult> Withdraw(string accountNo, [FromBody] decimal amount, string description)
        {
            try
            {
                var newBalance = await _accountService.WithdrawAsync(accountNo, amount, description);

                if (newBalance >= 0)
                {
                    return Ok(new { Message = "Withdrawal successful", NewBalance = newBalance });
                }
                else
                {
                    return BadRequest(new { Message = "Withdrawal failed: Insufficient funds" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }

}
