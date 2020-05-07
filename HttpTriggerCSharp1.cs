using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class OrderQueueItem
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
    }

    public static class HttpTriggerCSharp1
    {
        [FunctionName("HttpTriggerCSharp1")]
        // [return: Queue("myqueue-items")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Queue("myqueue-items")] out OrderQueueItem newQueueItem,
            ILogger log)
        {
            // https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Ccsharp%2Cbash
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            string mobileNumber = req.Query["mobileNumber"];

            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // string requestBody = new StreamReader(req.Body).ReadToEnd();

            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name ??= data?.name;
            // mobileNumber ??= data?.mobileNumber;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name} with {mobileNumber}. This HTTP triggered function executed successfully.";

            newQueueItem = new OrderQueueItem { Name = name, MobileNumber = mobileNumber };
            // newQueueItemJson = JsonConvert.SerializeObject(newQueueItem);
            // outputQueueItem.Add(JsonConvert.SerializeObject(newQueueItem));
            // await outputQueueItem.AddAsync(newQueueItem);

            return new OkObjectResult(responseMessage);
        }
    }
}
