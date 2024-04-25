using BudgetAnalyzer.Services.Interfaces;
using BudgetAnalyzer.Services.Logic;
using BudgetAnalyzer.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BudgetAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Route("GetTransactionTemplates")]
        public IActionResult GetTransactionsTemplates()
        {
            var templates = _transactionService.GetTransactionFileMappingsTemplates();
            return Ok(JsonSerializer.Serialize<IEnumerable<TransactionFileMapping>>(templates));
           
        }

        [HttpPost]
        [Route("CreateTransactionMapping")]
        public IActionResult CreateTransactionMapping([FromBody] TransactionFileMapping mapping)
        {
            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }
            var templates = _transactionService.CreateTransactionMapping(mapping);
            return Ok();
        }

    }
}
