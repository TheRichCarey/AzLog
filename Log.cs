using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace yer.AzLog
{
    public static class Log
    {
        [FunctionName("Log")]
        public static async Task<IActionResult> AcceptLogRequest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Log")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("HTTP trigger fired for log entry.");

            // Read from the NLOG format, like.
            // <parameter name='timestamp' type='System.String' layout='${longdate}'/>
            // <parameter name='loggerName' type='System.String' layout='${logger}'/>
            // <parameter name='loggerLevel' type='System.String' layout='${level}'/>
            // <parameter name='message' type='System.String' layout='${message}'/>
            string timestamp = req.Form["timestamp"]; 
            string loggerName = req.Form["loggerName"]; 
            string loggerLevel = req.Form["loggerLevel"]; 
            string message = req.Form["message"]; 
           
            var res = $"{timestamp}   | {loggerName} | {loggerLevel.ToUpper()} | {message}";
            log.LogInformation(res);

            //TODO: Persist the data

            return (ActionResult)new OkObjectResult(res);

        }
    }
}
