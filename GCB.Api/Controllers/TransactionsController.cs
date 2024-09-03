using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

using System;
using System.Linq;
using System.Threading.Tasks;
using GCB.Api.Services;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

namespace ProductService.Controllers
{
    [Route("odata/[controller]")]
    public class TransactionsController : ODataController
    {
        private readonly IGenericService<Transaction> _service;
        private readonly IMapper _mapper;

        public TransactionsController(IGenericService<Transaction> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<TransactionDto> Get()
        {
            var transactions = _service.GetAllAsync("Category", "BankAccount", "Attachments").Result;
            return transactions.Select(t => _mapper.Map<TransactionDto>(t)).AsQueryable();
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] Guid key)
        {
            var transaction = await _service.GetByIdAsync(key, "Category", "BankAccount", "Attachments");
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TransactionDto>(transaction));
        }

        public async Task<IActionResult> Post([FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = _mapper.Map<Transaction>(transactionDto);
            var createdTransaction = await _service.AddAsync(transaction);
            return Created(_mapper.Map<TransactionDto>(createdTransaction));
        }

        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] TransactionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = _mapper.Map<Transaction>(transactionDto);
            var updatedTransaction = await _service.UpdateAsync(key, transaction);
            return Updated(_mapper.Map<TransactionDto>(updatedTransaction));
        }

        public async Task<IActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<TransactionDto> transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _service.GetByIdAsync(key);
           
            if (transaction == null)
            {
                return NotFound();
            }
            ;
            var patchedTransaction=transactionDto.Patch(_mapper.Map<TransactionDto>(transaction));
           
            await _service.PatchAsync(patchedTransaction.Id, _mapper.Map<Transaction>(patchedTransaction));

            return Updated(_mapper.Map<TransactionDto>(patchedTransaction));
        }


        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var result = await _service.DeleteAsync(key);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
