using BudgetAnalyzer.CsvMapping;
using BudgetAnalyzer.Services.Interfaces;
using BudgetAnalyzer.Services.Logic;
using BudgetAnalyzer.Shared.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace BudgetAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public FilesController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("UploadTransactions")]
        public async Task<IActionResult> UploadTransactions([FromForm] IEnumerable<IFormFile> files)
        {
            var mappingDictionary = new Dictionary<string, string>();
            if (Request.Headers["TransactionFileMappingId"].Count > 0)
            {
                if (long.TryParse(Request.Headers["TransactionFileMappingId"], out long result))
                {
                    var mapping = _transactionService.GetTransactionFileMappingTemplate(result);
                    if (mapping != null)
                    {
                        mappingDictionary.Add("DateOfTransaction", mapping.TransactionDate);
                        mappingDictionary.Add("Description", mapping.Description);
                        if (!string.IsNullOrEmpty(mapping.AmountPaid))
                        {
                            mappingDictionary.Add("AmountPaid", mapping.AmountPaid ?? "");
                        }
                        if (!string.IsNullOrEmpty(mapping.AmountReceived))
                        {
                            mappingDictionary.Add("AmountReceived", mapping.AmountReceived ?? "");
                        }
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            if (mappingDictionary.Count == 0 || mappingDictionary == null)
            {
                return NotFound();
            }


            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                using (StreamReader sr = new StreamReader(stream))
                using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    var transactionMap = new DefaultClassMap<Transaction>();

                    transactionMap.Map(mappingDictionary);

                    csv.Context.RegisterClassMap(transactionMap);
                    var records = csv.GetRecords<Transaction>();
                    await _transactionService.ProcessTransactions(records);

                }
            }

            return Ok();
        }
    }

    public static class CsvHelperExtensions
    {
        public static void Map<T>(this ClassMap<T> classMap, IDictionary<string, string> csvMappings)
        {
            foreach (var mapping in csvMappings)
            {
                var property = typeof(T).GetProperty(mapping.Key);

                if (property == null)
                {
                    throw new ArgumentException($"Class {typeof(T).Name} does not have a property named {mapping.Key}");
                }
                
                if (property.PropertyType == typeof(decimal))
                {
                    classMap.Map(typeof(T), property).Name(mapping.Value).TypeConverter<CustomDecimalConverter>();
                }
                else
                {
                    classMap.Map(typeof(T), property).Name(mapping.Value);
                }
            }
        }

        public class CustomDecimalConverter : DecimalConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                text = text.Replace("$", "").Replace("(", "-").Replace(")", "");
                if (decimal.TryParse(text, out var result))
                {
                    return result;
                }
                else
                {
                    return decimal.Zero;
                }
            }
        }
    }
}
