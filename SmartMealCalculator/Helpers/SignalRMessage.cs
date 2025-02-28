using Newtonsoft.Json;

namespace SmartMealCalculator
{
    public class SignalRMessage
    {
        [JsonProperty("arguments")]
        public string Message { get; set; }
    }
}
