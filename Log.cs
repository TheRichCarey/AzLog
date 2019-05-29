using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using yer.AzLog.Model;

namespace yer.AzLog
{
    public static class Log
    {
        [FunctionName("Log")]
        public static void AcceptLogRequest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Log")] HttpRequest req,
            [CosmosDB(
                databaseName: "azlogger",
                collectionName: "azlogger-logs",
                ConnectionStringSetting = "MyCosmosDBConnection",
                Id = "{sys.randguid}",
                PartitionKey ="/loggerName"
            )]
            out LogDetail logDetail,
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
           
            var res = $"{timestamp} | {loggerName} | {loggerLevel.ToUpper()} | {message}";
            log.LogInformation(res);
            
            // Create a new Customers object
            logDetail = new LogDetail();
            // Create a new empty customer list on the customers object
            logDetail.Timestamp = timestamp;
            logDetail.LogLevel = loggerLevel;
            logDetail.LogName = loggerName;
            logDetail.Message = message;                  
    


        }
    }
}
