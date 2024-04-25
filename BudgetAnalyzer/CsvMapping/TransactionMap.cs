using BudgetAnalyzer.Shared.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BudgetAnalyzer.CsvMapping
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap() {

            Map(m => m.DateOfTransaction).Name("TransactionDate");
            Map(m => m.AmountPaid).Name("AmountPaid").TypeConverter<CustomDecimalConverter>();
            Map(m => m.Description).Name("Description");
        }
    }

    public class CustomDecimalConverter : DecimalConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            text = text.Replace("$", "").Replace("(", "-").Replace(")","");
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
