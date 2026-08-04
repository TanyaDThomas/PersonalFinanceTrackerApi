using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerApi.Contracts;
using PersonalFinanceTrackerApi.DTOs;

namespace PersonalFinanceTrackerApi.Controllers
{
    [ApiController]
    [Route("api/accounttypes")]

    public class AccountTypeController : ControllerBase
    {

        private readonly IAccountTypeCommandService _commandService;
        private readonly IAccountTypeQueryService _queryService;

        public AccountTypeController(IAccountTypeCommandService commandService, IAccountTypeQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allAccountTypes = await _queryService.GetAllAsync();
            return Ok(allAccountTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var accountTypeById = await _queryService.GetTypeByIdAsync(id);
            return Ok(accountTypeById);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountTypeDto dto)
        {
            var accountType = await _commandService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = accountType.Id },
                    accountType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateAccountTypeDto dto)
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
