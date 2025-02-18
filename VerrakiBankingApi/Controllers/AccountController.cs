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

        // Endpoint to get the balance of an account
        [HttpGet("balance/{accountId}")]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            try
            {
                var balance = await _accountService.GetBalanceAsync(accountId);
                return Ok(new { AccountId = accountId, Balance = balance });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Endpoint to deposit money into an account
        [HttpPost("deposit/{accountId}")]
        public async Task<IActionResult> Deposit(int accountId, [FromBody] DepositRequest request)
        {
            try
            {
                // Call DepositAsync and get the updated balance
                var newBalance = await _accountService.DepositAsync(accountId, request.Amount, request.Description);

                // Check if deposit was successful (assuming any non-negative balance indicates success)
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


        // Endpoint to withdraw money from an account
        [HttpPost("withdraw/{accountId}")]
        public async Task<IActionResult> Withdraw(int accountId, [FromBody] decimal amount, string description)
        {
            try
            {
                // Call WithdrawAsync and get the updated balance
                var newBalance = await _accountService.WithdrawAsync(accountId, amount, description);

                // Check if withdrawal was successful based on the balance
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
