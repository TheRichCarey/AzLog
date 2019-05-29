# Azlog

Azure Function in C#, designed to be an endpoint for NLOG remote logging using NLOGs Web Service logging target.

This is the final output of the blog post:  <a href="http://yer.ac/blog/2019/05/29/remote-nlog-logging-with-azure-functions-part-one/" target="_blank">here</a>

## Creating the settings file
To test locally, or deploy you will need to create the excluded file `local.settings.json` at the root.

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "MyCosmosDBConnection": "<your connection string>"
    }
}
```

## Updating NLOG targets
See the blog post for information, but essentially:

```xml
<target type='WebService'
            name='azurelogger'
            url='http://localhost:7071/api/Log'
            protocol='HttpPost'
            encoding='UTF-8'   >
      <parameter name='timestamp' type='System.String' layout='${longdate}'/>
      <parameter name='loggerName' type='System.String' layout='${logger}'/>
      <parameter name='loggerLevel' type='System.String' layout='${level}'/>
      <parameter name='message' type='System.String' layout='${message}'/>
    </target>
```
and then have one of your NLOG loggers use the `azurelogger` target.
The URL should be changed to a localhost URL for local testing, or the azurewebsites url for Azure deployment.
