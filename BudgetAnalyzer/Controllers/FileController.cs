using BudgetAnalyzer.CsvMapping;
using CsvHelper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace BudgetAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpPost]
        [Route("UploadTransactions")]
        public async Task<IActionResult> UploadTransactions([FromForm] IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                using (StreamReader sr = new StreamReader(stream))
                using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TransactionMap>();
                    var records = csv.GetRecords<TransactionCsv>().ToList();
                    Console.WriteLine(records.Count.ToString());
                }
            }

            return Ok();
        }
    }
}
