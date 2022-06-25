using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using botFunction.Models;
using System.Text.Json;

namespace botFunction
{
    public class Function1
    {
        const string INVALIDSTOCK = "Invalid stock name";

        [FunctionName("Function1")]
        public async Task Run([QueueTrigger("stock")] string message, ILogger log)
        {
            var queueMessage = JsonSerializer.Deserialize<QueueMessage>(message);

            var stockUrl = $"{Environment.GetEnvironmentVariable("StockUrl")}{queueMessage.Text}&f=sd2t2ohlcv&h&e=csv";

            Stream stream = await stockUrl.GetStreamAsync();
            StreamReader sr = new StreamReader(stream);

            sr.ReadLine(); //skip header
            string line = sr.ReadLine();
            var csvRow = new CSVRow(line);

            queueMessage.SenderUser = "bot";

            if (csvRow.Date is null)
            {
                queueMessage.Text = INVALIDSTOCK;
                log.LogInformation(INVALIDSTOCK);
            }
            else
            {
                queueMessage.Text = $"{csvRow.Symbol} quote is ${csvRow.GetQuote()} per share";
                queueMessage.ConnectionId = String.Empty;
            }

            var url = Environment.GetEnvironmentVariable("NotificationEndpoint");
            await url.PostJsonAsync(queueMessage);

            log.LogInformation($"C# Queue trigger function processed");
        }
    }
}
