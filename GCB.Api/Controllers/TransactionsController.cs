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

namespace GCB.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ODataController
    {
        private readonly IGenericService<Transaccion> _service;
        private readonly IMapper _mapper;

        public TransactionsController(IGenericService<Transaccion> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transactions = await _service.GetAllAsync("Categoria", "CuentaBancaria", "Adjuntos");
                var transactionDtos = transactions.Select(t => _mapper.Map<TransaccionDto>(t)).AsQueryable();
                return Ok(transactionDtos);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromODataUri] Guid key)
        {
            try
            {
                var transaction = await _service.GetByIdAsync(key, "Categoria", "CuentaBancaria", "Adjuntos");
                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TransaccionDto>(transaction));
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] TransaccionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var transaction = _mapper.Map<Transaccion>(transactionDto);
                var createdTransaction = await _service.AddAsync(transaction);
                return Created(_mapper.Map<TransaccionDto>(createdTransaction));
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut()]
        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] TransaccionDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var transaction = _mapper.Map<Transaccion>(transactionDto);
                var updatedTransaction = await _service.UpdateAsync(key, transaction);
                if (updatedTransaction == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TransaccionDto>(updatedTransaction));
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPatch()]
        public async Task<IActionResult> Patch([FromODataUri] Guid key, [FromBody] Delta<TransaccionDto> transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var transaction = await _service.GetByIdAsync(key, "Categoria", "CuentaBancaria", "Adjuntos");
                if (transaction == null)
                {
                    return NotFound();
                }

                var transactionDtoToPatch = _mapper.Map<TransaccionDto>(transaction);
                transactionDto.Patch(transactionDtoToPatch);

                var patchedTransaction = _mapper.Map<Transaccion>(transactionDtoToPatch);
                await _service.PatchAsync(key, patchedTransaction);

                return Updated(_mapper.Map<TransaccionDto>(patchedTransaction));
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            try
            {
                var result = await _service.DeleteAsync(key);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
