using Newtonsoft.Json;

namespace CurrencyConverter.Application
{
    public class CurrencyConversionDto
    {
        [JsonProperty("t")] public string Target { get; set; }

        [JsonProperty("s")] public string Source { get; set; }

        [JsonProperty("ta")] public int TargetAmount { get; set; }

        [JsonProperty("sa")] public int SourceAmount { get; set; }
    }
}
