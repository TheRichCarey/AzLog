using Newtonsoft.Json;

namespace yer.AzLog.Model
{
    public class LogDetail{
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp;
        [JsonProperty("logName")]
        public string LogName;

        [JsonProperty("logLevel")]
        public string LogLevel;

        [JsonProperty("message")]
        public string Message;
    }
}