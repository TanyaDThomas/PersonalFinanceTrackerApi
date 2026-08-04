using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;

namespace PersonalFinanceTrackerApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCommandService _commandService;
        private readonly IAccountQueryService _queryService;

        public AccountController(IAccountCommandService commandService, IAccountQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allAccounts = await _queryService.GetAllAccountsAsync();
            return Ok(allAccounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var accountById = await _queryService.GetAccountsByIdAsync(id);
            return Ok(accountById);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            var account = await _commandService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = account.Id },
                account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAccountDto dto)
        {
            await _commandService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commandService.DeleteAsync(id);
            return NoContent();
        }
    }
}
