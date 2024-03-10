using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BudgetAnalyzer.CsvMapping
{
    public class TransactionMap : ClassMap<TransactionCsv>
    {
        public TransactionMap() {

            Map(m => m.DateOfTransaction).Name("Date");
            Map(m => m.Amount).Name("Amount").TypeConverter<CustomDecimalConverter>();
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
