using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTracker.Application.Contracts;
using PersonalFinanceTracker.Application.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace PersonalFinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/transactiontypes")]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeCommandService _commandService;
        private readonly ITransactionTypeQueryService _queryService;

        public TransactionTypeController(ITransactionTypeCommandService commandService, ITransactionTypeQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allTypes = await _queryService.GetAll();
            return Ok(allTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var typeById = await _queryService.GetById(id);
            return Ok(typeById);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionTypeDto dto)
        {
            var transactionType = await _commandService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = transactionType.Id },
                transactionType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTransactionTypeDto dto)
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
