using BudgetAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public AllocationController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
    }
}