using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Application.Contracts;
using PersonalFinanceTracker.Application.DTOs;

namespace PersonalFinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionCommandService _commandService;
        private readonly ITransactionQueryService _queryService;

        public TransactionController(ITransactionCommandService commandService, ITransactionQueryService queryService)
        {
             _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TransactionQueryParameters parameters)
        {
            if(parameters.Page < 1)
            {
                return BadRequest("Page does not exist.");
            }

            if(parameters.PageSize < 0 || parameters.PageSize > 100)
            {
                return BadRequest("Page size does not exist.");
            }

            var allTransactions = await _queryService.GetAllAsync(parameters);
            return Ok(allTransactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transactionById = await _queryService.GetByIdAsync(id);
            return Ok(transactionById);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            var transaction = await _commandService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = transaction.Id },
                transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTransactionDto dto)
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
