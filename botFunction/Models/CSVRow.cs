using System;
using System.Linq;
using botFunction.Extensions;

namespace botFunction.Models
{
    public class CSVRow
    {
        public CSVRow(string line)
        {
            string[] values = line.Split(',');

            Symbol = values[0];
            Date = values[1].ToDatetime()?.Date;
            Time = values[2].ToDatetime()?.TimeOfDay;
            Open = values[3].ToDouble();
            High = values[4].ToDouble();
            Low = values[5].ToDouble();
            Close = values[6].ToDouble();
            Volume = values[7].ToLong();
        }

        public string Symbol { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        public double? Open { get; set; }

        public double? High { get; set; }

        public double? Low { get; set; }

        public double? Close { get; set; }

        public long? Volume { get; set; }

        public double GetQuote()
        {
            var values = new double[] { Open.Value, High.Value, Low.Value, Close.Value };
            return Queryable.Average(values.AsQueryable());
        }
    }
}
