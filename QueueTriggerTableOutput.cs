using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Company.Function
{
    // public static class QueueTriggerTableOutput
    // {
    //     [FunctionName("QueueTriggerTableOutput")]
    //     public static void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
    //     {
    //         log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    //     }
    // }

    // https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Ccsharp%2Cbash
    public static class QueueTriggerTableOutput
    {
        [FunctionName("QueueTriggerTableOutput")]
        // [return: Table("outTable", Connection = "AzureWebJobsStorage")]
        [return: Table("outTable")]
        public static Person Run(
            // [QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] 
            [QueueTrigger("myqueue-items")]
            JObject order,
            ILogger log)
        {
            try
            {
                return new Person
                {
                    PartitionKey = "Orders",
                    RowKey = Guid.NewGuid().ToString(),
                    Name = order["Name"].ToString(),
                    MobileNumber = order["MobileNumber"].ToString()
                };

            }
            catch (Exception ex)
            {
                log.LogError(ex, "Something Bad Happened");
                throw;
            }
        }
    }

    public class Person
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
    }
}
